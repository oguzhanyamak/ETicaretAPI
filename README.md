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
	* Persistence
		*  Veritabanı operasyonlar�
		* Application alt�nda bulunan servis aray�zlerinin ger�eklendi�i aland�r
		* DbContext
		* Migrations
		* Configurations
		* Seeding
* Presentation 
	* Kullanici erişim katmanidir


 ## Async özelliği olmayan metotlar için
> await Task.Run(() => Table.Update(data)); 
>
> await Task.Run(() => Table.Remove(model));
