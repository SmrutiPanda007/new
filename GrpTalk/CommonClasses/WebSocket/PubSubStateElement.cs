using System;
using System.Threading;
using System.Collections.Generic;

/// <summary>
/// Summary description for PubSubStateElement
/// </summary>
public class PubSubStateElement
{
    HashSet<WebSocketConfSubHandler> subscribers;
    int inProgressCount;
    int handRiseCount;
    int unMuteCount;
    public PubSubStateElement()
    {
        subscribers = new HashSet<WebSocketConfSubHandler>();
        inProgressCount = 0;
    }

    public Boolean AddSubscriber(WebSocketConfSubHandler subHandler)
    {
        subscribers.Add(subHandler);
        return subscribers.Contains(subHandler);
    }

    public void RemoveSubscriber(WebSocketConfSubHandler subHandler)
    {
        subscribers.Remove(subHandler);
    }

    public void BroadcastToSubscribers(String message)
    {
       
        foreach (WebSocketConfSubHandler subscriber in subscribers)
        {
            //try { 
            subscriber.Send(message);
            //}
            //catch ()
            //{
                
            //}
        }
       //subscribers.Broadcast(message);
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