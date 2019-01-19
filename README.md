![Logo](https://raw.githubusercontent.com/LorenzCK/github-coauth-tool/master/graphics/gitcoauth-logo-128.png)

# GitCoAuth

![Badge](https://img.shields.io/nuget/v/dotnet-gitcoauth.svg)

GitCoAuth is a **simple .NET&nbsp;Core Global Tool** that will help you generate **co-authorship commit lines**, as you should.

## Usage

After installing, run it passing your co-author's *GitHub username* as a parameter.
For instance:

```
gitcoauth lorenzck
```

will output the following line (and it will also automatically try to copy it to your clipboard):

```
Co-authored-by: Lorenz Cuno Klopfenstein <lorenzck@users.noreply.github.com>
```

Append the line to your commit message [following this GitHub guide](https://github.blog/2018-01-29-commit-together-with-co-authors/) (that is leaving at least one empty line between the commit message and the co-authorship trailer).
Once committed and pushed to GitHub, you and your co-authors will all have ownership of the commit.&nbsp;✌

## Installation

Run the following command from shell:

```
dotnet tool install -g dotnet-gitcoauth
```

To uninstall the tool again:

```
dotnet tool uninstall -g dotnet-gitcoauth
```
