var FormDropzone=function(){return{init:function(){Dropzone.options.myDropzone={init:function(){this.on("addedfile",function(n){var t=Dropzone.createElement("<button class='btn btn-sm btn-block'>Remove file<\/button>"),i=this;t.addEventListener("click",function(t){t.preventDefault();t.stopPropagation();i.removeFile(n)});n.previewElement.appendChild(t)})}}}}}()