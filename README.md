# Chill Updater

Chill updator is a updator made for my other [project](https://github.com/ChobbyCode/Update-Checker). Place the application in a folder in your application called "Chill". In the main directory of your application (not the Chill folder) create a new file called _config.json. Set the file up as followed:

```json
{
  "URL": "https://github.com/ChobbyCode/Update-Checker"
}
```

Change the url to a github repo. In that repo create a new branch and name it "Download", in the download branch you will put all the download files for updates here.

In that branch create a UC_config.json, this config file will be used by Chill to know where it should move the files.

> ### '*' means to get the data relative to the download folder.

> ### '..' means the data is relative to the main application.

FileName should be equal to the file from the end of from.

Below the json says:

Take the README.md file from the download folder and place it in the main app directory.

```json
{
    "ExtractInfo": {
        "Data": [
            {
                "FileName": "README.md",
                "From": "*/README.md",
                "To": "../"
            }
        ]
    }
}
```

You can also move multiple files & specify the folders they go into...

```json
{
    "ExtractInfo": {
        "Data": [
            {
                "FileName": "README.md",
                "From": "*/README.md",
                "To": "../"
            },

            {
                "FileName": "Another-File.txt",
                "From": "*/Another-File.txt",
                "To": "../A Folder/"
            }
        ]
    }
}
```

You can also move files around from the main directory

```json
{
    "ExtractInfo": {
        "Data": [
            {
                "FileName": "README.md",
                "From": "../README.md",
                "To": "../UsefulInformation/"
            }
        ]
    }
}
```
