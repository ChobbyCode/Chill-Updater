# Chill Updater

Chill updater is a updater made for my other [project](https://github.com/ChobbyCode/Update-Checker).

## Setup

In your application folder, create a new folder called "Chill". In the folder called Chill place the the Chill application. In your application folder, create a _config.json. In the config file, set it up as below

```json
{
  "URL": "https://github.com/ChobbyCode/Update-Checker"
}
```
> ### Remember to change the url (in the same format) to your github repo.

If you have not already created a github repo for your project, create a github repo and create a new branch. This branch has to be called "Download", in this download branch put the download files. WOW!!

In the "Download" branch create a new file UC_config.json, this is the last thing we need to do to setup Chill.

The basic JSON template is listed below:

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

Using this JSON file we can specify what happens to files when they are downloaded. We can say where files are supposed to be moved, what files are to be deleted.

Below is a example of a peice from the data part.

```json
"Data": [
  {
      "FileName": "README.md",
      "From": "*/README.md",
      "To": "../"
  }
]

```

In the file name part we specify what the name of the file is. In the from we specify where the file is moving from and then in to we specify where the files will be moving to.

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
