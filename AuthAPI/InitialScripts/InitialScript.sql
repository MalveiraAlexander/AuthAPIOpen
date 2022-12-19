INSERT INTO public."Roles"
("Name", "Description", "DeleteAt")
VALUES('System Owner', 'User owner of the system', null);

INSERT INTO public."Permissions"
("Name", "IsEnable", "DeleteAt", "RoleId")
VALUES('All Access', true, null, 1);

INSERT INTO public."People"
("FirstName", "LastName", "Picture", "Country", "State", "City", "CP", "YearOfBirth", "DeleteAt")
VALUES('System', 'Owner', null, 'Argentina', 'State', 'City', '0000', null, null);

INSERT INTO public."Users" 
("UserName", "Email", "Phone", "PasswordHash", "NewUserHash", "TokenRecovery", "PersonId", "CreatedAt", "UpdatedAt", "DeleteAt", "LastLogin")
VALUES('admin', 'example@test.com', '124565655', null, 'qwerty123', null, 1, NOW(), null, null, null);

INSERT INTO public."RoleUser"
("RolesId", "UsersId")
VALUES(1, 1);