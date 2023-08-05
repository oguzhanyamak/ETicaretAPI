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
		* Servis Aray�zleri [ IRepository ]
* Infrastructure
	* Infrastructure
		* Veritaban� d���ndaki operasyonlar� / servisleri / i�lemleri y�r�t�ld��� katmand�r.	
		* E-mail
		* Sms
		* Notification
		* Payment
	* Persistence
		* Veritaban� operasyonlar�
		* Application alt�nda bulunan servis aray�zlerinin ger�eklendi�i aland�r
		* DbContext
		* Migrations
		* Configurations
		* Seeding
* Presentation 
	* Kullan�c� eri�im katman�d�r