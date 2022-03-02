# netstatPlus
An Simple C# Written Libary to get all Active connections (TCP/UDP)


![image](https://media.discordapp.net/attachments/931150864729657395/948669503322066964/unknown.png?width=1440&height=361)

What can be used for:
- Tracing people on omegle
- Getting people's ip with anydesk
- And many more!


# Documents 📚:
first load the libary by using
`using netstatPlus;`
And if you want try out this code:
```
List<Connection> connections = netclient.GetConnections();


            
foreach (var connection in connections)
  {
   Console.WriteLine("---\nPID: " + connection.pid + "\nRemote: " + connection.remoteaddy + "\n---");
  }
```


## The **"Connection"** Object ⬛

## Remote 📤:
### "remoteaddy" (string)
The public IP of the remote connection

### "remoteport" (string)
The port used by the remote connection

## Local 📥:
### "localaddy" (string)
The public IP of the local connection

### "localeport" (string)
The port used by the local connection

## Process 🔌:
### "pid" (string)
the process id

### "name" (string)
the process name

## Other 🧩:
### "state" (string)
Indicates the state of a TCP connection, including:
- CLOSE_WAIT
- CLOSED
- ESTABLISHED
- FIN_WAIT_1
- FIN_WAIT_2
- LAST_ACK
- LISTEN
- SYN_RECEIVED
- SYN_SEND
- TIMED_WAIT


## Methods 🖱:
### "GetGetByID()" (Connection List)
Get a process connections by his id

parameters:
- `id` (string) (required)

returns List<Connection> (A list of the Connection Object)
  
![bar](https://cdn.discordapp.com/attachments/895632161057669180/930131378379554876/void_default_bar.PNG)

  
### "GetGetByName()" (Connection List)
Get a process connections by his name

parameters:
- `id` (string) (required)

returns List<Connection> (A list of the Connection Object)
  
![bar](https://cdn.discordapp.com/attachments/895632161057669180/930131378379554876/void_default_bar.PNG)
  
  
  
  
  
### "GetConnections()" (Connection List)
Get a all processes connections

parameters:
- `STATES` (string[]) (optional) - Get only connections that has one of the states in the array
- `NAME` (string) (optional) - Get only connections with a specific name
- `ID` (string) (optional) - Get only connections with a specific pid

returns List<Connection> (A list of the Connection Object)
  
![bar](https://cdn.discordapp.com/attachments/895632161057669180/930131378379554876/void_default_bar.PNG)

