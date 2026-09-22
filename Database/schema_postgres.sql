CREATE TABLE Users (
    UserId INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    UserName VARCHAR(150) NOT NULL,
    UserLastName VARCHAR(150) NOT NULL,
    UserPass VARCHAR(200) NOT NULL,
    UserEmail VARCHAR(200) NOT NULL
);

CREATE TABLE Account (
    AccountId INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    AccountName VARCHAR(200) NOT NULL,
    UserId INT NOT NULL REFERENCES Users(UserId),
    Ammount DECIMAL(18, 2) NOT NULL DEFAULT 0
);

CREATE TABLE AmmountType (
    AmmountTypeId INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    AmmountTypeDesc VARCHAR(250) NOT NULL,
    UserId INT
);

CREATE TABLE Category (
    CategoryId INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    CategoryDesc VARCHAR(300) NOT NULL,
    UserId INT NOT NULL REFERENCES Users(UserId)
);

CREATE TABLE SubCategory (
    SubCategoryId INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    SubCategoryDesc VARCHAR(300) NOT NULL,
    CategoryId INT NOT NULL REFERENCES Category(CategoryId)
);

CREATE TABLE DebtType (
    DebtTypeId INT PRIMARY KEY,
    DebtTypeDesc VARCHAR(100) NOT NULL
);

CREATE TABLE Debt (
    DebtId INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    DebtName VARCHAR(250) NOT NULL,
    StartAmount DECIMAL(18, 2) NOT NULL,
    Date TIMESTAMP NOT NULL,
    CategoryId INT NOT NULL REFERENCES Category(CategoryId),
    SubCategoryId INT NOT NULL REFERENCES SubCategory(SubCategoryId),
    DebtTypeId INT NOT NULL REFERENCES DebtType(DebtTypeId),
    UserId INT NOT NULL REFERENCES Users(UserId),
    DebtorName VARCHAR(250),
    CurrentAmount DECIMAL(18, 2) NOT NULL,
    CompletedDate TIMESTAMP
);

CREATE TABLE DebtPayment (
    DebtPaymentId INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    DebtId INT NOT NULL REFERENCES Debt(DebtId),
    AccountId INT NOT NULL REFERENCES Account(AccountId),
    Date TIMESTAMP NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL
);

CREATE TABLE ItemType (
    ItemTypeId INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    ItemTypeDesc VARCHAR(250) NOT NULL,
    UserId INT
);

CREATE TABLE PeriodType (
    PeriodTypeId INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    PeriodTypeDesc VARCHAR(250) NOT NULL,
    UserId INT
);

CREATE TABLE Item (
    ItemId INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    ItemName VARCHAR(250) NOT NULL,
    ItemDesc TEXT,
    Ammount DECIMAL(18, 2) NOT NULL,
    Periodity INT,
    StartDate TIMESTAMP NOT NULL,
    EndDate TIMESTAMP NOT NULL,
    Cancelled BOOLEAN NOT NULL DEFAULT FALSE,
    CategoryId INT NOT NULL REFERENCES Category(CategoryId),
    SubCategoryId INT REFERENCES SubCategory(SubCategoryId),
    ItemTypeId INT NOT NULL REFERENCES ItemType(ItemTypeId),
    AmmountTypeId INT NOT NULL REFERENCES AmmountType(AmmountTypeId),
    PeriodTypeId INT NOT NULL REFERENCES PeriodType(PeriodTypeId),
    UserId INT NOT NULL REFERENCES Users(UserId),
    AccountId INT NOT NULL REFERENCES Account(AccountId)
);

CREATE TABLE ItemPayment (
    ItemPaymentId INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    ItemId INT NOT NULL REFERENCES Item(ItemId),
    PaymentDate TIMESTAMP NOT NULL,
    Ammount DECIMAL(18, 2) NOT NULL
);

CREATE TABLE Transfer (
    TransferId INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    CategoryId INT NOT NULL REFERENCES Category(CategoryId),
    SubCategoryId INT REFERENCES SubCategory(SubCategoryId),
    PeriodTypeId INT NOT NULL REFERENCES PeriodType(PeriodTypeId),
    UserId INT NOT NULL REFERENCES Users(UserId),
    OriginAccountId INT NOT NULL REFERENCES Account(AccountId),
    DestinationAccountId INT NOT NULL REFERENCES Account(AccountId),
    TransferName VARCHAR(100) NOT NULL,
    TransferDesc TEXT,
    Ammount DECIMAL(18, 2) NOT NULL,
    Periodity INT,
    StartDate TIMESTAMP NOT NULL,
    EndDate TIMESTAMP NOT NULL,
    Cancelled BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE TransferPayment (
    TransferPaymentId INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    TransferId INT NOT NULL REFERENCES Transfer(TransferId),
    PaymentDate TIMESTAMP NOT NULL,
    Ammount DECIMAL(18, 2) NOT NULL
);
