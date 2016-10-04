Update 04 Oct 2016

I found the cause of most of the performance problems. Using Debug.WriteLine in the Progress handlers in the UI thread was causing massive slowdown.

The Progress bar kind of works. It is not being updated when scanning the directory, only on the thread which deals with pulling from the queue.

I added an example of one unit test; unfortunately it is not quite working yet as I need to provide an abstraction for FileInfo.

I wanted to have the process which watches the queue time out after x seconds of the queue being empty. Right now, it seems to terminate when it gets a null file from the queue, which means it kind of works but I am not sure why.

If I was to spend more time on this exercise, I would split the Processor class in the DirectoryProcessor class library into 2 classes, one being responsible for scanning the file system and one for processing items on the queue.

