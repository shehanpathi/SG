terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.0.0"
    }
  }
}

# Configure the Microsoft Azure Provider
provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "sg-rg" {
  name     = "sg-test"
  location = "East US"
}

resource "azurerm_virtual_network" "sg-vn" {
  name                = "sg-virtual-network"
  location            = azurerm_resource_group.sg-rg.location
  resource_group_name = azurerm_resource_group.sg-rg.name
  address_space       = ["10.123.0.0/16"]


  tags = {
    environment = "Dev"
  }
}


resource "azurerm_mysql_flexible_server" "sg-mysql-server" {
  name                   = "sg-mysql-server"
  resource_group_name    = azurerm_resource_group.sg-rg.name
  location               = azurerm_resource_group.sg-rg.location
  administrator_login    = "shehan"
  administrator_password = "admin@123"
  sku_name               = "B_Standard_B1s"
  zone                   = "1"
}

resource "azurerm_mysql_flexible_database" "sg-mysql-db" {
  name                = "sakila"
  resource_group_name = azurerm_resource_group.sg-rg.name
  server_name         = azurerm_mysql_flexible_server.sg-mysql-server.name
  charset             = "utf8"
  collation           = "utf8_unicode_ci"

}

resource "azurerm_mysql_flexible_server_firewall_rule" "sg-mysql-firewall-rule" {
  name                = "sg-mysql-firewall-rule-1"
  resource_group_name = azurerm_resource_group.sg-rg.name
  server_name         = azurerm_mysql_flexible_server.sg-mysql-server.name
  start_ip_address    = "0.0.0.0"
  end_ip_address      = "255.255.255.255"
}

resource "random_integer" "ri" {
  min = 10000
  max = 99999
}

resource "azurerm_cosmosdb_account" "sg-cosmos-acc" {
  name                = "sg-cosmos-${random_integer.ri.result}"
  location            = azurerm_resource_group.sg-rg.location
  resource_group_name = azurerm_resource_group.sg-rg.name
  offer_type          = "Standard"
  kind                = "MongoDB"

  enable_automatic_failover = true

  capabilities {
    name = "EnableAggregationPipeline"
  }

  capabilities {
    name = "mongoEnableDocLevelTTL"
  }

  capabilities {
    name = "MongoDBv3.4"
  }

  capabilities {
    name = "EnableMongo"
  }

  consistency_policy {
    consistency_level       = "BoundedStaleness"
    max_interval_in_seconds = 300
    max_staleness_prefix    = 100000
  }

  geo_location {
    location          = "eastus"
    failover_priority = 1
  }

  geo_location {
    location          = "westus"
    failover_priority = 0
  }

}


resource "azurerm_cosmosdb_mongo_database" "sg-cosmos-db" {
  name                = "sg-cosmos-db"
  resource_group_name = azurerm_cosmosdb_account.sg-cosmos-acc.resource_group_name
  account_name        = azurerm_cosmosdb_account.sg-cosmos-acc.name
  throughput          = 400
}


# Create the Linux App Service Plan
resource "azurerm_service_plan" "sg-sp" {
  name                = "sg-app-${random_integer.ri.result}"
  location            = azurerm_resource_group.sg-rg.location
  resource_group_name = azurerm_resource_group.sg-rg.name
  os_type             = "Linux"
  sku_name            = "F1"
}

# Create the web app, pass in the App Service Plan ID
resource "azurerm_linux_web_app" "sg-app-service" {
  name                = "sg-app-service-${random_integer.ri.result}"
  location            = azurerm_resource_group.sg-rg.location
  resource_group_name = azurerm_resource_group.sg-rg.name
  service_plan_id     = azurerm_service_plan.sg-sp.id
  https_only          = true
  site_config {
    always_on         = false // Required for F1 plan (even though docs say that it defaults to false)
    use_32_bit_worker = true  // Required for F1 plan
  }
  tags = {
    "environment" = "development"
  }
}