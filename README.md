# Main Idea

**Domain subject**\
We have companies which are employers\
We have persons who are employees

**Requirements**
- White list:
We know exactly all business logic of creating companies, fetching companies and getting companies details

- Gray list:
But we don't know anything about the business logic of getting employees from the particular company.

Imagine that this is a part of knowledge of some legacy system or in general words external resource.

# What we are going to achieve

We want to build modern up-to date application which allows to implement our well-known business and let perform bad-known logic in the old system.

Old system parts will be reimplemented in the further steps.

But at present moment of time we are going to use them as external resources.

# IDesign

**This repository contains the answer on this question.**

It includes source code for the architecture concept of IDesign method.

The services and the applications that host them are explained in further detail [here](./docs/architecture.md).

Some useful debug tools can be found [here](./docs/tye.md)

# Repository overview

## API sources

*White list. Our own applications that allow to perform explicitly work with our own sources:*

- [Application API](./src/apps/API)\
Just provides a simple methods of CRUD operations on the company or person

*Gray list. Two externals stand-alone web applications:*

- [Application Red](./src/apps/externals/Poc.Method.AppRedAPI)\
Just provides a simple method of getting all employees in the company from some external resource

- [Application Yellow](./src/apps/externals/Poc.Method.AppYellowAPI)\
Just provides a simple method of creating new company of some external resource

## Web

- [Web application](./src/apps/Web)\
Simple web application that allows to use our IDesign pattern concept.\
For the user it looks like he acts with only one source

## Services

- [Managers contracts](./src/contracts/managers)\

- [Managers](./src/services/managers)\

Describe the contracts of Managers

Implement the main chain of IDesign pattern:

- Managers are being devided by Domain Subject:
  - [Company Managers](./src/services/managers/Poc.Method.CompanyManagerService)\
  - [Person Managers](./src/services/managers/Poc.Method.PersonManagerService)\

## Resource accessors

- [Resource accessors contracts](./src/contracts/resource-access)\

- [Resource-accessors](./src/services/resource-access)\

Describe the contracts of Managers

Mainly these accessors allow to perform actions on our own resources or externals

- Accessors are being devided by our knowledges of how many sources we have:
  - [Red resource](./src/services/resource-access/Poc.Method.Service.ExternalAppRedAccess)\
  - [Yellow resource](./src/services/resource-access/Poc.Method.Service.ExternalAppYellowAccess)\
  - [Our own resource](./src/services/resource-access/Poc.Method.Service.ContextStorageAccess)\

Here we know that:
- Red and Yellow are externals and we use HttpClient to reach out remote hosts.
- Our own resource works directly with our own database (SQL)

## Data access layers

- [Dal](./src/dal/resource-access)\
Provides low access to the SQL database and is being used by our own resource accessors


# How to run

Just run `tye run` and that's it=)