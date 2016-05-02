# git2ai
Replaces placeholders in AssemblyInfo.cs with information form a git repository. Quick and dirty solution for internal use at schwindelig.ch. Use at your own risk.

Requires .NET Framework 4.6.1

## Placeholders
There are currently 2 placeholders you can use:

### {describe-tags}
Result: v0.0.1-pre-alpha-4-g03530aa

Is equal to git describe --tags --always

### {dirty}
Result: -dirty

Takes the "--dirty" part from --git describe --always --dirty. Appends "-dirty" if dirty, otherwise empty.

## Usage
### Installation
- Build with VS
- Copy git2ai.exe, CommandLine.dll, Libgit2sharp.dll and the NativeBinaries folder to a directory of your choice.
- Call git2ai with the params specified below.

### Parameters
 -g, --gitdir             Required. Path to .git directory

  -a, --assemblyinfodir    Required. Root to search for AssemblyInfo.cs files

  -r, --recursive          (Default: true) If true: Includes sub directories
                           from assemblyinfodir

  -p, --searchpattern      (Default: *AssemblyInfo.cs) Search pattern to use
                           for finding AssemblyInfo files
  
### Example
Basic: git2ai -g d:\moonstone\\.git -a d:\moonstone

Different search pattern: git2ai -g d:\moonstone\\.git -a d:\moonstone -p *GlobalAssemblyInfo.cs
 
Non recursive search: git2ai -g d:\moonstone\\.git -a d:\moonstone -r false
