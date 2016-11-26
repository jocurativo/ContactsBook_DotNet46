define({ "api": [
  {
    "type": "post",
    "url": "/api/contacts",
    "title": "Add a new contact",
    "name": "AddContact",
    "group": "Contacts",
    "version": "1.0.0",
    "description": "<p>Add a new contact</p>",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "FirstName",
            "description": "<p>First name</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "LastName",
            "description": "<p>Last name</p>"
          },
          {
            "group": "Parameter",
            "type": "Array",
            "optional": false,
            "field": "Emails",
            "description": "<p>List of emails</p>"
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK",
          "type": "json"
        }
      ]
    },
    "error": {
      "examples": [
        {
          "title": "Error-Response:",
          "content": "HTTP/1.1 400 Bad Request\n{\n \"Message\": \"Email example@gmail.com is already in use\"\n}",
          "type": "json"
        }
      ]
    },
    "filename": "Controllers/ContactsController.cs",
    "groupTitle": "Contacts"
  },
  {
    "type": "get",
    "url": "/api/contacts/:id",
    "title": "Get contact details",
    "name": "GetContactDetails",
    "group": "Contacts",
    "version": "1.0.0",
    "description": "<p>Get the contact details</p>",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "Integer",
            "optional": false,
            "field": "id",
            "description": "<p>Contact ID.</p>"
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "Success-Response:",
          "content": "   HTTP/1.1 200 OK\n{\n  \"ContactId\": 1,\n  \"FirstName\": \"First Name\",\n  \"LastName\": \"Last Name\",\n  \"Emails\": [\n        \"one@gmail.com\", \n        \"second@gmail.com\"\n  ]\n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "examples": [
        {
          "title": "Error-Response:",
          "content": "HTTP/1.1 404 Not Found",
          "type": "json"
        }
      ]
    },
    "filename": "Controllers/ContactsController.cs",
    "groupTitle": "Contacts"
  },
  {
    "type": "get",
    "url": "/api/contacts",
    "title": "Get the list of contacts",
    "name": "GetContacts",
    "group": "Contacts",
    "version": "1.0.0",
    "description": "<p>Get the list of contacts</p>",
    "success": {
      "examples": [
        {
          "title": "Success-Response:",
          "content": "   HTTP/1.1 200 OK\n[{\n\"ContactId\":1,\n\"FirstName\":\"First Name 1\",\n\"LastName\":\"Last Name 1\",\n},\n{\n\"ContactId\":2,\n\"FirstName\":\"First Name 2\",\n\"LastName\":\"Last Name 2\",\n},\n.....\n]",
          "type": "json"
        }
      ]
    },
    "filename": "Controllers/ContactsController.cs",
    "groupTitle": "Contacts"
  },
  {
    "type": "delete",
    "url": "/api/contacts",
    "title": "Remove a contact",
    "name": "RemoveContact",
    "group": "Contacts",
    "version": "1.0.0",
    "description": "<p>Remove a contact</p>",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "Integer",
            "optional": false,
            "field": "id",
            "description": "<p>Contact ID</p>"
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK",
          "type": "json"
        }
      ]
    },
    "error": {
      "examples": [
        {
          "title": "Error-Response:",
          "content": "HTTP/1.1 400 Bad Request\n{\n \"Message\": \"Invalid contact id\"\n}",
          "type": "json"
        }
      ]
    },
    "filename": "Controllers/ContactsController.cs",
    "groupTitle": "Contacts"
  },
  {
    "type": "put",
    "url": "/api/contacts",
    "title": "Update a contact",
    "name": "UpdateContact",
    "group": "Contacts",
    "version": "1.0.0",
    "description": "<p>Update a contact</p>",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "Integer",
            "optional": false,
            "field": "ContactId",
            "description": "<p>Contact ID</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "FirstName",
            "description": "<p>First name</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "LastName",
            "description": "<p>Last name</p>"
          },
          {
            "group": "Parameter",
            "type": "Array",
            "optional": false,
            "field": "Emails",
            "description": "<p>List of emails</p>"
          }
        ]
      }
    },
    "success": {
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK",
          "type": "json"
        }
      ]
    },
    "error": {
      "examples": [
        {
          "title": "Error-Response:",
          "content": "HTTP/1.1 400 Bad Request\n{\n \"Message\": \"Email example@gmail.com is already in use\"\n}",
          "type": "json"
        }
      ]
    },
    "filename": "Controllers/ContactsController.cs",
    "groupTitle": "Contacts"
  }
] });
