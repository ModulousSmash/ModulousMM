import('System')
import('System.Collections.Generic')
import('System.Linq')
import('System.Text')
import('System.Threading.Tasks')
import('System.IO')
function CreativityWrite(message)
	local splitted_message = message:split('#')
	if splitted_message[0] == "WARN" then
		Console.WriteLine("Lua Warning: " .. splitted_message[1])
    elseif splitted_message[0] == "b" then print("bee")
	else Console.WriteLine(splitted_message[0])
    end
	Console.WriteLine("RANRAN")
end