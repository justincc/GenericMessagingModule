# GenericMessagingModule #

## Introduction ##
An example region module to show generic messaging to and from viewers within OpenSimulator
    
At the moment, this will receive a GenericMessagePacket UDP message that has the
method "test".

## Sending a generic message to the simulator ##

One way to insert such a message from the client end is to use the TestClient
facility in libopenmetaverse [1].  This requires the bleeding edge
libopenmetaverse at the time of writing (2014-06-19).

After running TestClient.exe, first log in an OpenSimulator account from its
command line, e.g.

> login Ima User mypassword http://localhost:9000

(you might need to whack enter to see the console prompt).

Then send a generic message such as 

> sendgeneric test oh my

"test" is the method name and "oh", "my" are parameters.  On the simulator
command line, you should see the module log the fact that it has received this
message.

[1] https://github.com/openmetaversefoundation/libopenmetaverse/

# vim: ts=4:sw=4:et:tw=80
