#  Boutique ASP.NET Core MVC (Projet TP)

Ce dépôt contient le code source d'une application de commerce électronique simplifiée, développée dans le cadre d'un travail pratique (TP) en utilisant le framework **ASP.NET Core MVC** et **Entity Framework Core**.

---

##  Fonctionnalités Implémentées

* **Gestion des Catégories** : CRUD (Création, Lecture, Mise à jour, Suppression) complet via les vues échafaudées.
* **Gestion des Produits** : CRUD complet pour les produits, incluant une relation avec les catégories.
* **Recherche et Tri** : Possibilité de rechercher des produits par nom et de trier la liste des produits.
* **Authentification et Autorisation** : (À ajouter si prévu).
* **Tests Unitaires** : Mise en place de tests unitaires pour le service Produit (voir `ProduitServiceTests.cs`).

---

##  Technologies Utilisées

* **Framework** : .NET 9.0 (ASP.NET Core MVC)
* **Base de Données** : MySQL via Pomelo.EntityFrameworkCore.MySql (ou MSSQL si le `DbContext` a été modifié).
* **ORM** : Entity Framework Core (EF Core)
* **Langage** : C#
* **Frontend** : HTML, CSS (Bootstrap), Razor.

---

##  Démarrage du Projet (Local)

Pour lancer ce projet sur votre machine :

### Prérequis

* .NET SDK 9.0 ou supérieur.
* Visual Studio 2022 ou Visual Studio Code.
* Un serveur MySQL (si la chaîne de connexion n'utilise pas In-Memory Database).

### Étapes

1.  **Cloner le dépôt :**
    ```bash
    git clone [https://github.com/amal04saidani/Boutique-ASPNet.git](https://github.com/amal04saidani/Boutique-ASPNet.git)
    cd Boutique/Boutique
    ```

2.  **Mettre à jour la base de données :**
    ```bash
    dotnet ef database update
    ```
    (Cette étape exécute les migrations pour créer la base de données et exécute le `DbSeeder` pour insérer des données initiales).

3.  **Lancer l'application :**
    ```bash
    dotnet run
    # ou
    dotnet watch run
    ```
4.  Ouvrez votre navigateur et naviguez vers `https://localhost:<PORT>` (par exemple, `https://localhost:7044`).
5.  Naviguer vers /Produits/Create (par exemple, `https://localhost:7044/Produits/Create`).
6.  Naviguer vers /Catégories/Create (par exemple, `https://localhost:7044/Catégories/Create`).

---

##  Contribution

Si vous souhaitez apporter des améliorations, veuillez soumettre une "Pull Request".

---
