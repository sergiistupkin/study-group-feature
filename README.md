# Study Group Feature

## Overview
This repository contains tests and scripts related to the new Study Group feature for students.

## Structure
- `sql/` – SQL scripts for querying study groups
- `docs/` – Test plan with written test cases and Test task
- `src/TestApp` – TestApp 
- `src/TestAppAPI` – TestAppAPI 
- `tests/ComponentTests` – NUnit controller tests
- `tests/UnitTests` – NUnit unit tests for StudyGroup model

## Assumptions of how the App works
Based on provided code we expect the App is a student web application that allows users to create, join, leave, search and view these groups.
Create:
- a user can create a Study Group for one subject only once
- the group name must be between 5 and 30 characters
- the subject must be Math, Chemistry or Physics
- the group must have the creation date for sorting/filtering
Join:
- any user can join existing groups for different subjects
- there are no limits on how many groups a user can join
- when a user joins, the reference is stored under StudyGroup users list
Leaving:
- user can leave a group
- user should be removed from StudyGroup's users list
View Study Groups:
- the user should see a list of all Study Groups
- the user should be able to filter the list by subject
- the list should support sorting the newest first or oldest first

## Choosen approach
- Unit Tests for checking small pieces of logic. (ex. does the group name validation work?; can we properly map subjects to enum).
- Component Testing (API) - here we test the API controller directly. (ex. if a group is created, does it return OK?; when a user joins/leaves, does the API respond correctly?).
- E2E Testing (Manual UI testing) - testing from a user perspective. Since we don't have UI automation, we can test UI test cases manually (ex. filtering feature: newest/oldest)

- There are included both positiive tests (ex. create a group with valid data, join group, etc.) and negative tests (ex. short/long names, duplicate group)
- There are edge test cases to validate exact number of characters (5char/30char)
- We used mocked data to simulate different situation since we don't have a real DB.

