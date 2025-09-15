# chef-master

## Summary
This repository hosts a very simple web site that performs the following:

* The user is prompted for a URL (to a site site with a recipe on it).
* The host will download the HTML of the site and sift it for the presence of any [schema.org compatible recipe data.](https://developers.google.com/search/docs/appearance/structured-data/recipe)
* If found, the data is then parsed and re-formatted into a new, single-page recipe based on the selected style.

Many recipe sites include schema.org compatible JSON data for [search engine optimisation](https://developers.google.com/search/docs/appearance/structured-data/intro-structured-data), but include an excessive amount of verbiage that is printer-unfriendly.

This web host will attempt to automatically distill the verbosity out of the page and leave you with just the basic recipe information.

## Installation / usage
If you use Docker or similar container management, you can simply [pull and run the latest package](https://github.com/orgs/gman-au/packages?repo_name=chef-master). 

## Additional adapters

### Groq (https://groq.com/)

If the host, for whatever reason, cannot find any recipe information from the site, you can configure a [Groq account](https://console.groq.com/home) to be used as an adapter.

When confirmed, the host will prompt Groq to perform a pass at the site, and see if it can extract similarly-structured recipe data.

To enable this adapter, simply provide the following environment variables on startup (either in .NET environment or Docker `-e` option):
| Name | Value |
|--|--|
| `GROQ_ENDPOINT` | The Groq endpoint, this will be `https://api.groq.com` |
| `GROQ_API_KEY` | Your own (personal) Groq API key

> [!NOTE]
> Each 'attempt' at doing this will incur a cost on your Groq account.


### Qrist (https://qrist.app/)

[Qrist repo](https://github.com/gman-au/qrist)

* `Qrist` contains an integration for [Todoist](https://www.todoist.com/), allowing you to add a QR code to the corner of the recipe page.
* When you scan this QR code, Qrist will prompt you to confirm a block-add of *all of the ingredients on the recipe page* to your Todoist list.
  * This can be a helpful shortcut when you print the recipe out and need a quick way of adding the recipe to your 'shopping list'.

To enable this adapter, simply provide the following environment variable on startup:
| Name | Value |
|--|--|
| `QRIST_ENDPOINT` | The Qrist endpoint, this will be `https://qrist.app` |

> [!TIP]
> You can host your own [local instance of Qrist](https://github.com/gman-au/qrist/pkgs/container/qrist%2Fqrist) as well, if required. 
> If you do so, make sure to adjust the `QRIST_ENDPOINT` accordingly.

