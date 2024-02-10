# Contributing

TypeScript CodeDOM provides a primary component "TypeScriptCodeProvider" and a few helper classes for developing TypeScript code generators. And code generators will generate codes that will be used in application developments. Therefore, it is essential to make sure that improvements and bug fixings won't cause the generated codes to break existing applications. And if some slight breaking changes in the generated codes are necessary, please make sure that application developers can fix the breaking changes at the minimum cost.


## Getting Started

It is presumed that you are alreadly familiar with common good practices for raising issues, forking, creating pull requests and testing when contributing to a non-trivial open source project hosted in GitHub or alike.

## Testing

Running the unit test suites in this repository and the integration test suites of other TypeScript code generators is essential during the development of your contribution.

### Level 1: Unit Testing

It is essential to pass the unit test suite:
* [Tests/TypeScriptCodeDomTestsCore](https://github.com/zijianhuang/TypeScriptCodeDOM/tree/master/Tests/TypeScriptCodeDomTestsCore)

### Level 2: Integration Testing with Code Generators

You use TypeScriptCodeProvider likely because you are developing a code generator, thus the unit test suite of your code generator is an integration test suite of TypeScript CodeDOM. 

#### Your Code Generator

It is critical that you craft a unit test suite for your code generator. Typically you would import a locally built Nuget package of TypeScript CodeDOM after making some changes and passing the unit testing of TypeScript CodeDOM. Then you run your unit test suite.

**Hints:**
* There are basically 3 ways of importing local Nuget package when developing the package for testing:
    1. [Local feeds](https://learn.microsoft.com/en-us/nuget/hosting-packages/local-feeds)
    1. or `Install-Package My.Package.Name -Source LocalFolder`
    1. or `Install-Package LocalFolder\My.Package.Name.1.2.3.nupkg`

#### Fonlow.Poco2TS

TypeScript CodeDOM had been started as a by-product of [WebApiClientGen](https://github.com/zijianhuang/webapiclientgen) which includes a CLI tool "[POCO2TS.exe](https://github.com/zijianhuang/webapiclientgen/wiki/POCO2TS.exe)". And the primary component of "POCO2TS.exe" is "[Fonlow.Poco2Ts](https://github.com/zijianhuang/webapiclientgen/wiki/Fonlow.Poco2Ts)".

2 Unit Test Suites:
* [Poco2TsTestsCore](https://github.com/zijianhuang/webapiclientgen/tree/master/Tests/Poco2TsTestsCore)
* [Poco2NgFormTests](https://github.com/zijianhuang/webapiclientgen/tree/master/Tests/Poco2NgFormTests)

![Poco2TsTests](https://github.com/zijianhuang/webapiclientgen/raw/master/Doc/Poco2TsTests.png)

Most unit test cases of these test suites are of integration testing for TypeScript CodeDOM.

It is optional however highly recommended that you run these 2 test suites to ensure no breaking changes and reduce the chance of the pull requests getting declined.

**Steps:**

1. Fork [WebApiClientGen](https://github.com/zijianhuang/webapiclientgen)
1. Build and ensure at lease these 2 test suites pass.
1. Import the locally built Nuget package of TypeScript CodeDOM, and run the tests again.


#### WebApiClientGen

**Steps:**

1. Fork [WebApiClientGen](https://github.com/zijianhuang/webapiclientgen)
1. Build and ensure [Tests/IntegrationTestsCore](https://github.com/zijianhuang/webapiclientgen/tree/master/Tests/IntegrationTestsCore) pass.
1. Import the locally built Nuget package of TypeScript CodeDOM.
1. Build "DemoCoreWeb".
1. Run "[CreateAspNetCoreClientApi.ps1](https://github.com/zijianhuang/webapiclientgen/blob/master/CreateAspNetCoreClientApi.ps1)" to generate Angular TypeScript codes.

2 Integration Test Suites:

* [Tests/IntegrationTestsCore](https://github.com/zijianhuang/webapiclientgen/tree/master/Tests/IntegrationTestsCore) to confirms the basic health of "DemoCoreWeb", a Web service for integration testing.
* [HeroesDemo/src/clientapi/WebApiNG2ClientAuto.spec.ts](https://github.com/zijianhuang/webapiclientgen/blob/master/HeroesDemo/src/clientapi/WebApiNG2ClientAuto.spec.ts) which used [WebApiCoreNG2FormGroupClientAuto.ts](https://github.com/zijianhuang/webapiclientgen/blob/master/HeroesDemo/src/clientapi/WebApiCoreNG2FormGroupClientAuto.ts) generated.

The first test suite will launch "DemoCoreWeb" hosted by local DotNet Kestrel then shutdown the Web service after all tests have been executed. 

![WebApiClientGenTests](https://github.com/zijianhuang/webapiclientgen/raw/master/Doc/WebApiClientGenTests.png)

To run the 2nd test suite:
1. Launch "DemoCoreWeb" using [StartDemoCoreWeb.ps1](https://github.com/zijianhuang/webapiclientgen/blob/master/StartDemoCoreWeb.ps1).
1. Under folder "HeroesDemo", run `ng test`.

![Angular Tests](https://github.com/zijianhuang/webapiclientgen/raw/master/Doc/ngTests.png)

**Hints:**

If your code generator is targeting a particular JavaScript/TypeScript library/framework utilizing jQuery, AXIOS, Fetch API or Aurelia, you may be interested in the following folders and running some integration tests:

* [aurelia](https://github.com/zijianhuang/webapiclientgen/tree/master/aurelia)
* [axios](https://github.com/zijianhuang/webapiclientgen/tree/master/axios)
* [fetchapi](https://github.com/zijianhuang/webapiclientgen/tree/master/fetchapi)
* [jQuery](https://github.com/zijianhuang/webapiclientgen/blob/master/DemoCoreWeb/README.md)

Your integration test suites for the generated TypeScript codes are probably similar to the test suites above.

#### OpenApiClientGen

**Steps:**

1. Fork [WebApiClientGen](https://github.com/zijianhuang/openapiclientgen)
1. Build and run all test suites except "SwagApiTests" and "SwagApiTestsTextJson".
1. Import the locally built Nuget package of TypeScript CodeDOM.
1. Build and run the test suites selected again.

![OpenApiClientGenTests](https://github.com/zijianhuang/openapiclientgen/raw/master/Docs/Images/TestSuitesInitial.png)

### Level 3: Integration Testing with Applications

The ultimate test is to ensure the generated codes work well in your applications interacting with local data or remote services like Web services. Some of the test cases of the unit test suites of your application components and the integration test suites of your application along with external resources are for integration testing of your code generators.

