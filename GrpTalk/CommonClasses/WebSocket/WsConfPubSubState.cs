using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Net.WebSockets;
using log4net;



public class WsConfPubSubState
{
    static ILog logger = log4net.LogManager.GetLogger("WSConferenceLogger");
    HashSet<WsConfSubscriber> subscribers;
    HashSet<WsConfSubscriber> disposedSubs;
	//ReaderWriterLock have to use instead of below;
    public ManualResetEvent hashSetChangeDone = new ManualResetEvent(true);
    public Int32 noOfBroadcasters = 0;
    public Int32 noOfModifiers = 0;
    Int32 inProgressCount;
    Int32 handRiseCount;
    Int32 unMuteCount;

    public WsConfPubSubState()
    {
        subscribers = new HashSet<WsConfSubscriber>();
    }
    public Boolean AddSubscriber(WsConfSubscriber subscriber)
    {
        bool result = false;
        lock (this)
        {
            if (noOfModifiers++ == 0)
            {
                hashSetChangeDone.Reset();
            }
            if (noOfBroadcasters > 0)
            {
                Monitor.Wait(this);
            }
        }
        try
        {
            logger.Info(subscribers + ";" + subscriber);
            result = subscribers.Add(subscriber);
        }
        finally
        {
            lock (this)
            {
                if (--noOfModifiers == 0)
                {
                    hashSetChangeDone.Set();
                }
            }
        }
        return result;
    }

    public void RemoveSubscriber(WsConfSubscriber subscriber)
    {
        lock (this)
        {
            if (noOfModifiers++ == 0)
            {
                hashSetChangeDone.Reset();
            }
            if (noOfBroadcasters > 0)
            {
                Monitor.Wait(this);
            }
        }
        try
        {
            subscribers.Remove(subscriber);
            if (disposedSubs != null) disposedSubs.Remove(subscriber);
        }
        finally
        {
            lock (this)
            {
                if (--noOfModifiers == 0)
                {
                    hashSetChangeDone.Set();
                }
            }
        }
    }

    public async Task BroadcastToSubscribers(String message)
    {
        ArraySegment<byte> outputBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
        hashSetChangeDone.WaitOne();
        lock (this)
        {
            ++noOfBroadcasters;
        }
        try
        {
            foreach (WsConfSubscriber subscriber in subscribers)
            {
                Boolean gotSignal = false;
                try
                {
                    if (subscriber.subSocket.State == WebSocketState.Open)
                    {
                    gotSignal = subscriber.autoREvent.WaitOne();
                    await subscriber.subSocket.SendAsync(outputBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    else
                    {
                        if (disposedSubs == null) disposedSubs = new HashSet<WsConfSubscriber>();
                        disposedSubs.Add(subscriber);
                    }
                }
                catch (System.ObjectDisposedException de)
                {
                    disposedSubs.Add(subscriber);
                    logger.Error("Error while BroadcastToSubscribers msg->" + message, de);
                }
                catch (Exception e)
                {
                    logger.Error("Error while BroadcastToSubscribers msg->" + message, e);
                }
                finally
                {
                    if (gotSignal) subscriber.autoREvent.Set();
                }
            }
            
        }
        finally
        {
            lock (this)
            {
                if (--noOfBroadcasters == 0)
                {
                    if (disposedSubs != null && disposedSubs.Count > 0)
                    {
                        foreach (WsConfSubscriber subscriber in disposedSubs)
                        {
                            logger.Info("Removing DisposedWebSocket..." + subscriber.GetHashCode() + " " + subscribers.Remove(subscriber));
                        }
                        disposedSubs = null;
                    }
                    Monitor.PulseAll(this);
                }
            }
            
        }
    }

    public int GetSubscribersCount()
    {
        return subscribers.Count;
    }

    public void IncrementInProgressCount()
    {
        Interlocked.Increment(ref inProgressCount);
    }

    public void DecrementInProgressCount()
    {
        if (Interlocked.Decrement(ref inProgressCount) == 0)
        {
            Interlocked.Exchange(ref unMuteCount, 0);
            Interlocked.Exchange(ref handRiseCount, 0);
        }

    }
    public int GetInProgressCount()
    {
        return inProgressCount;
    }
    public void IncrementUnMuteCount()
    {
        Interlocked.Increment(ref unMuteCount);
    }
    public void DecrementUnMuteCount()
    {
        if (Interlocked.Decrement(ref unMuteCount) == -1)
        {
            Interlocked.Exchange(ref unMuteCount, 0);
        }


    }
    public int GetUnMuteCount()
    {
        return unMuteCount;
    }
    public void IncrementHandRiseCount()
    {
        Interlocked.Increment(ref handRiseCount);
    }

    public void DecrementHandRiseCount()
    {
        if (Interlocked.Decrement(ref handRiseCount) == -1)
        {
            Interlocked.Exchange(ref handRiseCount, 0);
        }
    }

    public int GetHandRiseCount()
    {
        return handRiseCount;
    }


}