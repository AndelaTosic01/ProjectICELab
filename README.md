# Detailed explanation is written on Project.ppx file

# MIntermediateRepresentation

This project implements a representation of manufacturing processes based on Regionalized Value State Dependence Graph (RVSDG).  


# Preliminares

##  Intermediate Representation

The term intermediate representation (IR) or intermediate
language designates the data-structure(s) used by the
compiler to represent the program being compiled.
Choosing a good IR is crucial, as many analyses and
transformations (e.g. optimizations) are substantially easier
to perform on some IRs than on others.
Most non-trivial compilers actually use several IRs during the
compilation process, and they tend to become more lowlevel as the code approaches its final form.

## Control Flow Graph
https://www.geeksforgeeks.org/software-engineering-control-flow-graph-cfg/?ref=lbp

# Data Flow Testing

https://www.geeksforgeeks.org/data-flow-testing/?ref=lbp

# RVSDG IR

The RVSDG is a dataflow centric intermediate representation (IR) where nodes 
represent computations, edges represent computational dependencies, and regions 
capture the hierarchical structure of programs. 
It represents programs in demand-dependence form, implicitly supports structured 
control flow, and models entire programs within a single IR.

### Types of Variables

- Integer
- Float
- String
<!--- - AutoResetEvent-->

### Simple Nodes

Simple nodes model primitive operations such as addition and subtraction.
They have an operator associated with them, and a node’s signature must correspond 
to the signature of its operator.
Simple nodes map their input value tuple to their output value tuple by evaluating 
their operator with the inputs as arguments, and associating the results with 
their outputs. 

#### Supported operations:

- sum(a, b)
- sub(a, b)
- mul(a, b)
- div(a, b)
- mod(a, b)
- sleep(seconds)
- return(value)
- a > b, a >= b
- a < b, a <= b
- a == b
- print(message)
- concat([s1,s2,...,sn])
- random(lower, upper)

### Gamma-Nodes

A 𝛾-node models a decision point and contains regions R0, ..., R𝑘 | 𝑘 > 0 of matching
signature. Its first input is a predicate, which determines the region under evaluation. 
It evaluates to an integer 𝑣 with 0 ≤ 𝑣 ≤ 𝑘. The values of all other inputs are 
mapped to the corresponding arguments of region R𝑣 , R𝑣 is evaluated, and the values 
of its results are mapped to the outputs of the 𝛾-node. 𝛾-nodes represent conditionals 
with symmetric control flow splits and joins, such as if-then-else or switch
statements without fall-throughs. It contains three regions: one for each case, and
a default region. 

### Theta-Nodes

A 𝜃 -node models a tail-controlled loop. It contains one region that represents 
the loop body.
The length and signature of its input tuple equals that of its output, or the 
region’s argument tuple. The first region result is a predicate. Its value 
determines the continuation of the loop. When a 𝜃 -node is evaluated, the
values of all its inputs are mapped to the corresponding region arguments and 
the body is evaluated. When the predicate is true, all other results are mapped 
to the corresponding arguments for the next iteration. Otherwise, the result 
values are mapped to the corresponding outputs. The loop body of an iteration 
is always fully evaluated before the evaluation of the next iteration. This 
avoids “deadlock“ problems between computations of the loop body and the 
predicate, and results in well-defined behavior for non-terminating loops that 
update external state.
𝜃-nodes permit the representation of do-while loops. In combination with 𝛾-nodes, 
it is possible to model head-controlled loops, i.e., for and while loops. Thus, 
employing tail-controlled loops as basic loop construct enables us to express 
more complex loops as a combination of basic constructs. This normalizes the 
representation and reduces the complexity of optimizations as there exists 
only one construct for loops. Another benefit of tail-controlled loops is that 
their body is guaranteed to execute at least once, enabling the unconditional 
hoisting of invariant code with side-effects.
When the predicate evaluates to true, the results for 𝑛 and 𝑟 of the current 
iteration are mapped to the region arguments to continue with the next iteration. 
When the predicate evaluates to false, the loop exits and the results are mapped
to the node’s outputs. We define a loop variable as a quadruple that represents 
a value routed through a 𝜃 -node:

<!---### Lambda-Nodes

A 𝜆-node models a function and contains a single region representing a function’s body. It
features a tuple of inputs and a single output. The inputs refer to external variables the 𝜆-node depends on, and
the output represents the 𝜆-node itself. The region has a tuple of arguments comprised of a function’s external
dependencies and its arguments, and a tuple of results corresponding to a function’s results.
An 𝑎𝑝𝑝𝑙𝑦-node represents a function invocation. Its first input takes a 𝜆-node’s output as origin, and all other
inputs represent the function arguments. In the rest of the paper, we refer to an 𝑎𝑝𝑝𝑙𝑦-node ’s first input as its
function input, and to all its other inputs as its argument inputs. Invocation maps the values of a 𝜆-node’s input
𝑘-tuple to the first 𝑘 arguments of the 𝜆-region, and the values of the function arguments of the 𝑎𝑝𝑝𝑙𝑦-node to
the rest of the arguments of the 𝜆-region. The function body is evaluated and the values of the 𝜆-region’s results
are mapped to the outputs of the 𝑎𝑝𝑝𝑙𝑦-node.--->

<!---### Delta-Nodes

A 𝛿-node models a global variable and contains a single region representing the constants’
value. It features a tuple of inputs and a single output. The inputs refer to the external variables the 𝛿-node
depends on, and the output represents the 𝛿-node itself. The region has a tuple of arguments representing a
global variable’s external dependencies and a single result corresponding to its right-hand side value.
Similarly to 𝜆-nodes, we define the context variable of a 𝛿-node. It provides the corresponding
input and argument for a variable a 𝛿-node depends on.-->

### Omega-Nodes (root)

An 𝜔-node models a translation unit. It is the top-level node of an RVSDG and has 
no inputs or outputs. It contains exactly one region. This region’s arguments 
represent entities that are external to the translation unit and therefore 
need to be imported. Its results mark all exported entities in the translation
unit. 

### Edges

Edges connect node outputs or region arguments to a node input or region result, 
and are either value typed, i.e., represent the flow of data between computations, 
or state typed, i.e., impose an ordering on operations with side-effects. State 
edges are used to preserve the observational semantics of the input program by 
ordering its side-effecting operations. In practice, a richer type system permits further 
distinction between different kind of values or states. For example, different 
types for fixed- and floating-point values helps to distinguish between these arithmetics, 
and a type for functions permits to correctly specify the output types of 𝜆-nodes 
and the function input of 𝑎𝑝𝑝𝑙𝑦-nodes 

### Example

![](doc/example1.png)

![](doc/example2.png)

### Respository 

- https://github.com/phate/jlm
- https://github.com/phate/jive

# Documentation

https://docs.microsoft.com/it-it/dotnet/csharp/language-reference/xmldoc/recommended-tags

# Coding Conventions 

https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions

# Benchmark

https://benchmarkdotnet.org/articles/overview.html

# Test 

https://docs.microsoft.com/it-it/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2022

# Git 

_A list of my commonly used Git commands_

## Getting & Creating Projects

| Command | Description |
| ------- | ----------- |
| `git init` | Initialize a local Git repository |
| `git clone ssh://git@github.com/[username]/[repository-name].git` | Create a local copy of a remote repository |

## Basic Snapshotting

| Command | Description |
| ------- | ----------- |
| `git status` | Check status |
| `git add [file-name.txt]` | Add a file to the staging area |
| `git add -A` | Add all new and changed files to the staging area |
| `git commit -m "[commit message]"` | Commit changes |
| `git rm -r [file-name.txt]` | Remove a file (or folder) |

<!--- 
## Branching & Merging

| Command | Description |
| ------- | ----------- |
| `git branch` | List branches (the asterisk denotes the current branch) |
| `git branch -a` | List all branches (local and remote) |
| `git branch [branch name]` | Create a new branch |
| `git branch -d [branch name]` | Delete a branch |
| `git push origin --delete [branch name]` | Delete a remote branch |
| `git checkout -b [branch name]` | Create a new branch and switch to it |
| `git checkout -b [branch name] origin/[branch name]` | Clone a remote branch and switch to it |
| `git branch -m [old branch name] [new branch name]` | Rename a local branch |
| `git checkout [branch name]` | Switch to a branch |
| `git checkout -` | Switch to the branch last checked out |
| `git checkout -- [file-name.txt]` | Discard changes to a file |
| `git merge [branch name]` | Merge a branch into the active branch |
| `git merge [source branch] [target branch]` | Merge a branch into a target branch |
| `git stash` | Stash changes in a dirty working directory |
| `git stash clear` | Remove all stashed entries |
-->

## Sharing & Updating Projects

| Command | Description |
| ------- | ----------- |
| `git push origin [branch name]` | Push a branch to your remote repository |
| `git push -u origin [branch name]` | Push changes to remote repository (and remember the branch) |
| `git push` | Push changes to remote repository (remembered branch) |
| `git push origin --delete [branch name]` | Delete a remote branch |
| `git pull` | Update local repository to the newest commit |
| `git pull origin [branch name]` | Pull changes from remote repository |
| `git remote add origin ssh://git@github.com/[username]/[repository-name].git` | Add a remote repository |
| `git remote set-url origin ssh://git@github.com/[username]/[repository-name].git` | Set a repository's origin branch to SSH |

<!--- ### Inspection & Comparison

| Command | Description |
| ------- | ----------- |
| `git log` | View changes |
| `git log --summary` | View changes (detailed) |
| `git log --oneline` | View changes (briefly) |
| `git diff [source branch] [target branch]` | Preview changes before merging |
-->

# Advanced Topics 

[Value Dependence Graphs: Representation Without Taxation](doc/vdg-popl94.pdf)

[Optimizing compilation with the Value State Dependence Graph](doc/UCAM-CL-TR-705.pdf)

[Compiling with the Regionalized Value State Dependence Graph](doc/reissmann_poster.pdf)

[Perfect Reconstructability of Control Flow from Demand Dependence Graphs](doc/2693261.pdf)

[RVSDG: An Intermediate Representation for Optimizing Compilers](doc/main.pdf)
