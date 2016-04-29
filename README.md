# git2ai
Tries to replace placeholders in AssemblyInfo.cs with git data. Use at your own risk.

## Placeholders
There are currently only 2 placeholders you can use:

### {describe-tags}
Result: v0.0.1-pre-alpha-4-g03530aa
Is equal to git describe --tags --always

### {dirty}
Result: -dirty
Takes the "--dirty" part from --git describe --always --dirty. Appends "-dirty" if dirty, otherwise empty.

## Usage
### Parameters
 -g, --gitdir             Required. Path to .git directory

  -a, --assemblyinfodir    Required. Root to search for AssemblyInfo.cs files

  -r, --recursive          (Default: true) If true: Includes sub directories
                           from assemblyinfodir

  -p, --searchpattern      (Default: *AssemblyInfo.cs) Search pattern to use
                           for finding AssemblyInfo files

  --help                   Display this help screen.
  
  ### Example