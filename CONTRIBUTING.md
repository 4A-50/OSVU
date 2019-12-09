# Contributing To OSVU
Thanks for having a look on how to contribute. This definitely not a solo project and any help I can get is more than appreciated.

If u have an idea of something that should be added, check the [roadmap](https://app.gitkraken.com/glo/board/XeTm5ecC6AAPsU5b "RoadMap") first just to make sure it’s not either already in development or planned.

# How To Contribute
## Testing
The best way to test OSVU is to just play it. Just download a copy of the files and play! Keep us in the loop is u find any bugs by submitting an issue (just check its not already one).

## Reporting Bugs
Obviously check that this bug isn’t already know and if it isn’t then good news, you get to submit a bug report!

### How To Submit A Good Bug Report:

[Create an issue](https://github.com/4A-50/OSVU/issues/new?template=bug_report.md) on the project's repository and provide the following information.

Explain the problem and include additional details to help someone reproduce the problem:
- Use a clear and descriptive title for the issue to identify the problem.
- Provide a simplified project that reproduces the issue whenever possible.
- Describe the exact steps which reproduce the problem in as many details as possible. For example, start by explaining how you used the project. When listing steps, don't just say what you did, but explain how you did it.
- Provide specific examples to demonstrate the steps. It's always better to get more information. You can include links to files or GitHub projects, code snippets and even print screens or animated GIFS. If you're providing snippets in the issue, use Markdown code blocks.
- Describe the behaviour you observed after following the steps and point out what exactly is the problem with that behaviour.
- Explain which behaviour you expected to see instead and why.
 -  If the problem wasn't triggered by a specific action, describe what you were doing before the problem happened and share more information using the guidelines below.

Also It Helps To Include details about your configuration and environment:
- Which version of the project are you using?
- What's the name and version of the OS you're using?
- Any other information that could be useful about your environment

## Submitting An Idea
Before submitting a new idea, please check the list of idea suggestions in the issue tracker and the roadmap as you might find out that you don't need to create one. When submitting an idea add as much detail as possible it’s a lot easier for someone to make it if they know 100% what u mean.

### How To Submit A Good Idea:

[Create an issue](https://github.com/4A-50/OSVU/issues/new?template=feature_request.md) on the project's repository and provide the following information:
- Use a clear and descriptive title for your idea.
- Provide a step-by-step description of the idea in as many details as possible.
- Provide specific examples to demonstrate the steps. It's always better to get more information. You can include links to files or GitHub projects, code snippets and even print screens or animated GIFS. If you're providing snippets in the issue, use Markdown code blocks.

## Submiting A Pull Request

Please send a pull request with a clear list of what you've done ([read more about pull requests](https://help.github.com/en/github/collaborating-with-issues-and-pull-requests/about-pull-requests)). It's helpful if u can include tests in the request as testing is something that allways needs to happen.

- Use a clear and descriptive title for the pull request to state the improvement you made to the code or the bug you solved. 
- Provide a link to the related issue if the pull request is a follow up of an existing bug report or enhancement suggestion. 
- Comment why this pull request represents an enhancement and give a rationale explaining why you did it that way and not another way.
- Use the same coding style as the one used in this project.
- Documentation: If your PR adds or changes any public properties or methods, you must retain the old versions preceded with `[Obsolete("Describe what to do / use instead")]` attribute wherever possbile, and you must update any relevant pages in the /docs folder. It's not done until it's documented!
- Welcome suggestions from anyone to improve your pull request.

Also please follow the coding conventions below.

Always write a clear log message for your commits. One-line messages are fine for small changes, but bigger changes should look like this:
```
$ git commit -m "A brief summary of the commit""
> 
> A paragraph describing what changed and its impact.
```

## Coding Conventions
- To indent we use a Tab (4 spaces)
- Simplistic code is always prefered, make code easy to read and follow (avoid magic)
- Always try and use simplest solution
- Always put spaces after list items and method parameters `([1, 2, 3]`, not `([1,2,3])`, around operators `(x += 1, not x+=1)`
- This is open source software. Consider the people who will read your code, and make it look nice for them. It's sort of like driving a car: Perhaps you love doing donuts when you're alone, but with passengers the goal is to make the ride as smooth as possible.
- Follow [C# standard naming conventions](https://github.com/ktaranov/naming-convention/blob/master/C%23%20Coding%20Standards%20and%20Naming%20Conventions.md). Also, be descriptive. 'NetworkIdentity identity', not 'NetworkIdentity uv' or similar. Avoid prefixes like m_, s_, or similar.
- Fields and methods in a class are private by default, no need to use the private keyword there.

## Final Words
Thanks For Helping Improve OSVU

4A-50

![OSVU Banner](https://imgur.com/jSxu49k.png "OSVU")
