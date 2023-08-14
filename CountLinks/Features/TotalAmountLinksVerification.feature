Feature: TotalAmountLinksVerification
    In order to verify the total amount of links
    As a user
    I want to compare the number of links on the page with the number of links in the file

Scenario Outline: VerifyTotalAmountOfLinks with key word "<keyword>"
    Given User is on the website "https://loveit.com.ua" with key word "<keyword>"
    When User count the total amount of links on the page
    When User count the total amount of links in the file
    Then User compare the total amount of links with the number of links in the file "D:\\links.txt"

    Examples:
    | keyword  |
    | серце    |
    | сережки  |
    | кільце   |
    | каблучка |
