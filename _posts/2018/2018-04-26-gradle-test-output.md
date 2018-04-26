---
layout: post
title: Improving test output in Gradle
author: Håvard Kindem
---
If you've ever ran tests in Gradle, you might have noticed that the output on failing tests are quite sub-par. This becomes an issue on Travis specifically, as you cannot debug the results and to get more infomation on the test, you must enable the `--stacktrace` flag. By default, Gradle does not output which tests have been run, something which could be useful.

Another thing I find useful is to see any console output. You could enable the `--info` or `--debug` flag, but I feel that Gradle clutters this too much. For my projects, I prefer using this test configuration to get the most information possible, without cluttering the output too badly. 

This will output anything written to the console (including lint errors), print the full stack traces and list all tests ran.

```groovy
tasks.withType(Test) {
    testLogging {
        exceptionFormat "full"
        showCauses true
        showExceptions true
        showStackTraces true
        showStandardStreams true
        events = ["passed", "skipped", "failed", "standardOut", "standardError"]
    }
}
```
