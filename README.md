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
		* Veritabanı dışındaki operasyonları / servisleri / işlemleri yürütüldüğü katmandır.	
		* E-mail
		* Sms
		* Notification
		* Payment
	* Persistence
		* Veritabanı operasyonları
		* Application altında bulunan servis arayüzlerinin gerçeklendiği alandır
		* DbContext
		* Migrations
		* Configurations
		* Seeding
* Presentation 
	* Kullanıcı erişim katmanıdır