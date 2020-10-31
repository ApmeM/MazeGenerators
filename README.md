#PluginTemplate
==========
PluginTemplate is a project structure template for MyONez plugin libraries

- Project contatins tests and main project with links to eachother.
- Create nuget package for .net
- Create nuget package for bridge.net

Just replace "PluginTemplate" everywhere (filenames and files content) and push to git.

Once pushed build github action will try to build it agains ,net and bridge.net and run .net tests

To create a release add a tag v1.0.0 to git ad push it.