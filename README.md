<h1 align="center">ClunkerBot</h1>

<p align="center">
  <img src="art/scrots/scrot001.png" />
</p>

<h2 align="center">Vehicular utilities for Telegram</h3>

### What can this do?

_(TODO)_

### What's under the hood?

_(TODO)_

### Can I run it myself?

Heck, yeah. It's so simple even an idiot can do it.

~~If you are an idiot, head over the [releases](https://github.com/electricduck/clunkerbot/releases) page for pre-built binaries. I'm not judging.~~

#### Prerequisites

 * **[.NET Core](https://dotnet.microsoft.com/download)** &mdash; At least version **2.1** of the **.NET Core SDK**
 * **[Git](https://git-scm.com/)** &mdash; _Duh._
 * **[Docker](https://www.docker.com/get-started)** _(Optional)_ &mdash; Yup, like all the cool software these days, you can run this in Docker too!
 * A few minutes of your precious time
 
#### Setup

    $ git pull https://github.com/electricduck/clunkerbot
    $ cd clunkerbot/src/ClunkerBot
    $ dotnet restore
    
#### Configuration

Got this far? Good stuff. Now let's set it up.

Copy `appsettings.json.example` to `appsettings.json` and modify it as follows:

    {
        "apiKeys": {
            "telegram": "..."
        },
        "config": {
            "awoo": {
                "word": "Bork. ",
                "repeat": false
            },
            "botUsername": "@CarPups_bot"
        }
    }
    
##### Configuration keys

 * `apiKeys`
   * `telegram` &mdash; Your bot API key. Set this up with [BotFather](https://t.me/botfather), who will give you an API like the nice gentleman he is. If you're confused, there's plenty of help on the Intertubes.
 * `config`
   * `awoo` &mdash; This is a dumb feature leftover from testing that I decided to keep because it amused me. It outputs the word configurd below on `/awoo`.
     * `word` &mdash; The word to output.
     * `repeat` &mdash; If set to true, the bot will keep repeating the word in a single message, incrementing it each time.
   * `botUsername` &mdash; The username you gave the bot. It's pretty imporant you set this right (commands will fail to work correctly otherwise), and don't forgot the **@**!
    
#### Build & Run

    $ dotnet run
    
You should see various messages spat out into your terminal. If all went well, you should see `✔️ Listening for triggers` &mdash; the bot is ready for work. If the application has panicked and run for the hills, and you swear it wasn't your own dumbass fault, [https://github.com/electricduck/clunkerbot/issues/new](create an issue).
    
##### Build Docker image _(Optional)_

Oh hey, you noticed a `Dockerfile` in the root. Good eye. Although mostly untested, it does run and broadly seem to work.

    $ docker build -t clunkerwatch .
    $ docker run -d --name clunkerbot clunkerbot -v /path/to/appsettings.json:/app/appsettings.json
    
#### Moment of Truth

Message your bot on Telegram and issue a command. Try `/awoo` or `/info`. You'll see a log of in/out messages along with errors in your terminal. Enjoy 😊.

### Who created this thing?

  * **[Ducky / ElectricDuck](https://github.com/electricduck)** &mdash; Lead developer. He makes shit happen.

### Gimme' the legal gubbins

This project is licensed under **MIT**. See (https://github.com/electricduck/clunkerbot/blob/master/LICENSE)[LICENSE] for details.
