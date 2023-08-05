# ETicaretAPI
# Onion Architecture
* Core
	* Domain
		* Entities
		* Value Object
		* Enumeration
		* Exceptions (Entity Exceptions )
	* Application
		* DTO
		* ViewModel
		* Mapping
		* Validators
		* CQRS Pattern
		* Servis Arayüzleri [ IRepository ]
* Infrastructure
	* Infrastructure
		* Veritabaný dýþýndaki operasyonlarý / servisleri / iþlemleri yürütüldüðü katmandýr.	
		* E-mail
		* Sms
		* Notification
		* Payment
	* Persistence
		* Veritabaný operasyonlarý
		* Application altýnda bulunan servis arayüzlerinin gerçeklendiði alandýr
		* DbContext
		* Migrations
		* Configurations
		* Seeding
* Presentation 
	* Kullanýcý eriþim katmanýdýr