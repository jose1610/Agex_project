


select	*
from	agex_employee_salary

insert into agex_employee_salary(SALARY_AMOUNT, CURRENCY_ID, CREATE_DATETIME, STATUS)
			values	('40000', 'GTQ', GETDATE(), 'A'),
					('17000', 'GTQ', GETDATE(), 'A')

select	*
from	agex_employee_workstation

insert into agex_employee_workstation(SALARY_ID, NAME_WORKSTATION, CREATE_DATETIME, STATUS)
			values	(1, 'GERENTE GENERAL', GETDATE(), 'A'),
					(2, 'PROGRAMADOR', GETDATE(), 'A')


select	*
from	agex_country

insert into agex_country(COUNTRY_NAME, CREATE_DATETIME, STATUS)
			values	('GUATEMALA', GETDATE(), 'A'),
					('EL SALVADOR', GETDATE(), 'A')


select	*
from	agex_state

insert into agex_state(STATE_NAME, CREATE_DATETIME, STATUS)
			values	('CIUDAD DE GUATEMALA', GETDATE(), 'A'),
					('QUETZALTENANGO', GETDATE(), 'A')

