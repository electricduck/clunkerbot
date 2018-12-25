<p align="center">
  <a href="https://t.me/clunkerbot">
    <img src="https://placekitten.com/80/80" width="80" height="80">
  </a>

  <h3 align="center">ClunkerBot</h3>

  <p align="center">
    Lots of handy vehicular utlities, provided over a bot on <a href="https://telegram.org">Telegram</a>.
    <br>
    <br>
    Message <a href="https://t.me/clunkerbot">@ClunkerBot</a>
    <br>
    <a href="https://github.com/electricduck/clunkerbot">Github</a> | <a href="https://electricduck.visualstudio.com/ClunkerBot">Azure DevOps</a> | <a href="https://electricduck.github.io/clunkerbot/">Github.io</a>
    <br>
    <br>
    <a href="https://electricduck.visualstudio.com/ClunkerBot/_build/latest?definitionId=1"><img src="https://electricduck.visualstudio.com/ClunkerBot/_apis/build/status/ClunkerBot%20-%20Production"></a>
  </p>
</p>

## What's this?

_(TODO)_

### Need help?

A list of commands can be obtained by messaging `/help` to <a href="https://t.me/clunkerbot">@ClunkerBot</a>. It's advised to not do this in a group, as the output can be long and disruptive &mdash; <a href="https://github.com/electricduck/clunkerbot/issues/8">issue #8</a> relates to this. You can also see help for individual commands by doing `/help <module>` _(e.g. `/help guessmileage`)_.

## Building

_(TODO)_

## Contributing

_(TODO)_

### Branch stratergy

There is a very strict branch stratergy, and any deviations will be noted &mdash; and dealth with as quickly as possible. Messy branches can lead to confusing development, especially to outsiders.

The big thing to note is development takes place in `develop`, not the default `master` branch, unlike most other repositories.

#### Branches

 * `master` &mdash; Production-ready code
   * Code is only ever merged from `develop`, with the last commit before the merge being "Version X.X.X` (which only includes changes to the files that provide versioning).
   * Changes to areas outside the software's code can be directly commited to here, and later merged into `develop`. This `README.md` file you're looking at usually is modified in the `master` branch, then merged into `develop` when changes are finished.
 * `develop` &mdash; Development/WIP code
    * This branch is allowed to be in a non-working state **if there is a good reason for it**.
    * Usually things are merged from `feature/*` into here, but commiting directly to it is perfectly acceptable (branching out for tiny feature changes and/or bugfixes is annoying and time-consuming).
 * `feature/*` &mdash; Features/buxfixes
    * This branch is generally used for large changes, and later merged into `master` &mdash; don't squash commits!
    * Always name this branch as `feature/{1}-{2}`, where:
      * `{1}` - The issue ID, padded by four zeroes _(e.g. `0014`, `0007`, `0194`)
      * `{2}` - A brief explanation of the issue (or the title if its deemed short enough), with spaces separated by `-`'s _(e.g. `my-awesome-feature`, `broken-thing-fix`, `include-pictures-of-cats`)_
    * To re-iterate, bugs can be done here.
* `hotfix/` &mdash; Hotfixes
  * Because of the rolling release process, this isn't used.
* `experimental` &mdash; Here be dragons
    
##### Notes

 * Commits that relate to issues should always start with `[#123] Commit message`. _Github_ will magically link it with the issue!
 * Commits messages are generally done in first-person and start with a verb (with the feature/bugfix optionally before it, serperated by a dash) _(e.g. `Add this thing`, `Feature - Fix broken thing`, `Feature 2 - Remove shitting thing`)_ &mdash; since it makes more sense &mdash; but deviations are allowed: enforcing commit message rules are irriating as fuck to developers.

_As a side-note, I encourage other projects to add a block of text similar to this. I see far too many projects with wild branch stratergies (or none at all &mdash; shame on you) and zero explanation of it._

## License

Copyright © 2018-2019 <a href="https://github.com/electricduck">Ducky</a>.

ClunkerBot is free software: you can redistribute, modify, and use it for commercial purpouses it under the terms of the <a href="https://github.com/electricduck/clunkerbot/blob/master/LICENSE">MIT license</a>.

ClunkerBot is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the MIT license for more details.
