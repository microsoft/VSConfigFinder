# [Visual Studio] VSConfig Finder

When you want to set up Visual Studio from a new environment, `.vsconfig` files can be very useful as they are easy to be created from your [existing installations](https://learn.microsoft.com/en-us/visualstudio/install/import-export-installation-configurations?view=vs-2022) or from a [working solution](https://devblogs.microsoft.com/setup/configure-visual-studio-across-your-organization-with-vsconfig/). However, one existing problem with the `.vsconfig` usage is that the Visual Studio Installer supports importing one `.vsconfig` file at a time, so if you have multiple `.vsconfig`s throughout your solution (e.g. you have a monorepo that is consisted of multiple projects) and you want to apply them while setting up your pipeline, you would have to run an installer operation as many times as you'd want to use the different `.vsconfig`s. One way to get around this problem is to generate a single `.vsconfig` yourself that you put on the root of the solution, but this approach still has its own issues: For example, if you're only interested in a subset of the solution, you'll install far more components than the ones you need, resulting in a longer install and subsequent update time.

_VSConfigFinder_ is designed to be a redistributable, single-file executable that can be used in build or deployment scripts to use multiple `.vsconfig`s that exist throughout the solution without having to go through multiple installer operations by recursively finding nested `.vsconfig` files and merging them into a single output file, or an installer command line argument, depending on the customer requirement.

_VSConfigFinder_ is a simple tool and will not be shipped with the Visual Studio Installer, so please feel free to consume the package based on your need and use it for your own deployment setup.

## Example

Imagine that you have a solution or a repository with the folder structure as below:

```
root
  - folderA
  - folderB
    - .vsconfig (packageE)
  - folderC
    - someProject
      - .vsconfig (packageA, packageB)
    - folderD
      - folderE
        - .vsconfig (pacakgeC)
      - folderF
        - .vsconfig (packageD)
```

If you want to pass in all the components that are needed to build & run `folderC`, you could run the tool with the following parameters:

`VSConfigFinder.exe --folderpath root\folderC`

This will generate the following command as a console output that you can simply pass into the installer.

`--add packageA --add packageB --add packageC --add packageD`

Remember to add your own verb (e.g. `install` or `modify`) in conjunction with the tool output.

## Multi-Root Folders Support

Say if you want to do something similar to the example above, but you want everything under `folderB` AND `folderC`. You cannot pass in one or the other, because the two do not share a common folder (if you pass in the `root`, `folderA` will also be included). Instead, you can simply pass in the topmost folders as a list to achieve your goal.

`VSConfigFinder.exe --folderpath root\folderC root\folderB`

This will generate the following command as a console output that you can simply pass into the installer.

`--add packageA --add packageB --add packageC --add packageD --add packageE`

Again, remember to add your own verb (e.g. `install` or `modify`) in conjunction with the tool output.

## Alternate Example

Alternatively, you can pass in additional parameters provided by the tool to get the merged `.vsconfig` as a single file:

`VSConfigFinder.exe --folderpath root\folderC --createfile --configoutputpath c:\somefolder`

This will generate an alternate single `.vsconfig` file with all the needed components in the specified `configOutputPath`. If you don't specify a `configOutputPath`, the output directory will default to the current directory.

Note that if you choose to use `--createfile`, the Visual Studio Installer arguments will no longer be output to the console.

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

## License

This project is licensed under the [MIT license](LICENSE.txt).

## Code of Conduct

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of Microsoft 
trademarks or logos is subject to and must follow 
[Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/en-us/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion or imply Microsoft sponsorship.
Any use of third-party trademarks or logos are subject to those third-party's policies.
