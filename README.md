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

## Usage 

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

In the file name part we specify what the name of the file is. In the "from" we specify where the file is moving from and then in "to" we specify where the files will be moving to.

> #### To get files out of the download we use '*'
> #### To get files out of the main application folder we use '..'

For example if we want to move the README file from the downloads to the main application folder we set the JSON up as below:

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

The JSON above takes the README file in the downloads and puts it the main program.

We can create new folders by just putting a new directory before where we access.

```json
{
    "ExtractInfo": {
        "Data": [
            {
                "FileName": "README.md",
                "From": "*/README.md",
                "To": "../Important/"
            }
        ]
    }
}
```

The JSON above takes the README.md and puts it in a new folder called Important.

If there is a file in the main directory and we want to move it into a new folder we can do this as seen below:

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

Above takes the README file in the main directory and puts it in a new folder called UsefulInformation

### Deleting Old Files

We can delete files by just specifying the location of the file.

```json
"Delete": [
  {
    "Location": "../README.md"
  }
]
```

The code above deletes the file README.md in the main program directory

## Example

Below is a full example of the JSON that will move a LICENSE file into a Important folder and add a new file to main directory called README.md and delete a old exe file.

```json
{
    "ExtractInfo": {
        "Data": [
            {
                "FileName": "LICENSE.txt",
                "From": "../LICENSE.txt",
                "To": "../Important/"
            },
            

            {
                "FileName": "README.md",
                "From": "*/README.md",
                "To": "../"
            }
        ],
        "Delete": [
            {
                "Location": "../AnOldExe.exe"
            }
        ]
    }
}
```
