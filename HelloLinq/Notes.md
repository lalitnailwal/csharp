# Notes

## IEnumerable
* IEnumerable is a forward only readonly collection cursor through a collection, we can ask for what is the current element and go to next one, if no next value then it returns null

* IEnumerable is a pull based approach which means the generator method is inactive untill someone shows interest in the value. It means we can build query and till very last and
when someone show interest (i:e for loop) the execution took place

* The opposite of pull based approach is pushed based approach. A push based aproach is observable pattern, where we have a publiser and and one subscriber

* The number generator method implemneted using IEnumberable with yeild keywork doesn't generate entire number in a go but it generate number one by one after which the no go through a sequence of
LINQ pipeling (Linq operators) for processing of the value at the very end of the query. This way helps in overcoming the memory leakage issue when dataset is quite huge'

* Linq Statement helps in doing query composition before its execution. which is also called deferred execution.