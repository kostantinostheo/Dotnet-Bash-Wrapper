# .Net Bash Wrapper

### Summary

<p><b>.Net Bash Wrapper</b> is a small set of C# methods that helps you execute cmd/terminal commands or .sh files using C# code. The Wrapper can be used on any .NET application or Unity application/game.</p>

--- 
### Source Files

| File      | Description |
| ----------- | ----------- |
| BashWrapper.cs    | Core Methods for Bash Wrapper       |
| BashManager.cs   | Managing and Simplifying Bash Wrapper Functionality        |

### Methods
#### Bash Manager

##### 1. RunAsync

<b>Description:</b> </br>
```Executes asynchronous cmd or terminal commands.</br>```

##### 2. RunSync

<b>Description:</b> </br>
```Executes synchronous cmd or terminal commands.</br>```

##### 3. BashAsExecutableAsync

<b>Description:</b> </br>
```Makes a bash file executable asynchronously</br>```

##### 4. BashAsExecutableSync

<b>Description:</b> </br>
```Makes a bash file executable synchronously</br>```

#### Bash Wrapper

##### 1. Run 

```c#
public static async Task<T> Run<T>(string command, bool debug = false)
```

<b>Description:</b> </br>
Executes a single cmd or terminal command.</br>
<b>Parameters:</b> </br>
- `string command`: The command to execute.
- `bool debug`: Whether or not to print logs.


##### 2. BashAsExecutable 

```c#
public static async Task<T> BashAsExecutable<T>(string bashFileName)
```

<b>Description:</b> </br>
Makes a bash (.sh) script executable.</br>
<b>Parameters:</b> </br>
- `string bashFileName`: The name of file we want to make executable.
