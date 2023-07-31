# Notes

## Process vs Thread
* A program or a logic runs in a process and one process can't share the memory of other process. on the other hand we can have multiple threads inside a process and each thread can access all the memory of entire process

* Processes are distribute acorss all the cpu's by operating system and they compete for CPU's

## Parallel programing
* A typical complex problem is split down into multiple independent units and one cpu take care of one calculationa and another cpu take care of another calculation and we run them in parallel and once job complete they are combined togethere to produce the result

* for parallel programing we need multiple cpu cores

## async programmmin
* For async programing multiple cpu cores are not needed

* A cpu can have a single core and capable of handling one task at a moment they are not capable of doing parallel calculations so in that case if there are some task which are not CPU bound such as DB opeations and network operations we can do them in parallel with the help of async programing because they will not limit the use of cpu

* Web server do have a pool of threads by default the no of threads in thread pool is equal to the no of cpu's, webserver don't create 1000 of threads because thread are heavy weight they are memory intensive task, so they can't grow indefinitely we have to keep no of thread in thread pool small.

* wehen a requst arrive to web server one thread form thread pool is assigned to it to handle the processing and once processing is complete thread gets back to the thread pool. 

* In case of async programing thread handle the http request and once this request starts processing soem network or db intensive task, thread got free to handle other request. once response of earlier network or db intensive task is ready another thread from thread pool is assigned to complete the processing.

* So Async programing ia always used wheneve we do any non cpu bound work i:e we access any file server, database, netwrok and if we program like that the web server will be efficient and they can handle much more request as compared to synchronous programing and locking threads programing

* dir > myfile.txt

* In c# 3.0 Linq was introduced

* in c# 4.0 task was introduced

* A task is just like thread but not exactly, A task is just a unit of work which is very lightweight. A task do contain status such as waiting to run, completed, running, faulted etc. and once a task is completed we will be having a result. The tasks get deployed on threadpool and once any thread get free it starts on task

* await says hey csharp if you have anything to do, do it, but once the task is finished continue form here downwards. await just schedule the rest of the methods and it will automatically called once the task is done.