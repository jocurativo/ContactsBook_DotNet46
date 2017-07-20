using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactsBook.Framework.Contracts.Factories;
using ContactsBook.Framework.Dtos;
using ContactsBook.Framework.Entities;
using ContactsBook.Framework.Exceptions;

namespace ContactsBook.API.Controllers
{
    public class ContactsController : ContactsBook.API.BaseController
    {
        public ContactsController(IServiceFactory factory)
            :base(factory)
        {}

        /**
         * @api {get} /api/contacts Get the list of contacts
         * @apiName GetContacts
         * @apiGroup Contacts
         * @apiVersion 1.0.0
         * @apiDescription  Get the list of contacts
         *
         * @apiSuccessExample Success-Response:
         *     HTTP/1.1 200 OK
         *  [{
         *  "ContactId":1,
         *  "FirstName":"First Name 1",
         *  "LastName":"Last Name 1",
         *  },
         *  {
         *  "ContactId":2,
         *  "FirstName":"First Name 2",
         *  "LastName":"Last Name 2",
         *  },
         *  .....
         *  ]
         */
        public IEnumerable<ContactInfoDto> Get()
        {
            return this.Factory.ContactsService.GetFiltered(string.Empty);
        }

        /**
         * @api {get} /api/contacts/:id Get contact details
         * @apiName GetContactDetails
         * @apiGroup Contacts
         * @apiVersion 1.0.0
         * @apiDescription  Get the contact details
         *
         * @apiParam {Integer} id Contact ID.
         *
         * @apiSuccessExample Success-Response:
         *     HTTP/1.1 200 OK
         *  {
         *    "ContactId": 1,
         *    "FirstName": "First Name",
         *    "LastName": "Last Name",
         *    "Emails": [
         *          "one@gmail.com", 
         *          "second@gmail.com"
         *    ]
         *  }
         *
         *
         * @apiErrorExample  Error-Response:
         *     HTTP/1.1 404 Not Found
         *
         */
        public IHttpActionResult Get(int id)
        {
            var contact = this.Factory.ContactsService.GetById(id);

            if (contact == null) 
                return NotFound() ;
            
            return Ok(contact);
        }

        /**
         * @api {post} /api/contacts Add a new contact
         * @apiName AddContact
         * @apiGroup Contacts
         * @apiVersion 1.0.0
         * @apiDescription  Add a new contact
         *
         * @apiParam {String} FirstName First name
         * @apiParam {String} LastName Last name
         * @apiParam {Array} Emails List of emails
         *
         * @apiSuccessExample Success-Response:
         *     HTTP/1.1 200 OK
         *
         *
         * @apiErrorExample  Error-Response:
         *     HTTP/1.1 400 Bad Request
         *     {
         *      "Message": "Email example@gmail.com is already in use"
         *     }
         *
         */
        public IHttpActionResult Post([FromBody]Contact contact)
        {
            try
            {
                this.Factory.ContactsService.Add(contact);
                return Ok();
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (EntityValidationException ex)
            {
                return BadRequest(string.Join(",", ex.Validations.Select(x => x.ErrorMessage)));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        /**
         * @api {put} /api/contacts Update a contact
         * @apiName UpdateContact
         * @apiGroup Contacts
         * @apiVersion 1.0.0
         * @apiDescription  Update a contact
         *
         * @apiParam {Integer} ContactId Contact ID
         * @apiParam {String} FirstName First name
         * @apiParam {String} LastName Last name
         * @apiParam {Array} Emails List of emails
         *
         * @apiSuccessExample Success-Response:
         *     HTTP/1.1 200 OK
         *
         *
         * @apiErrorExample  Error-Response:
         *     HTTP/1.1 400 Bad Request
         *     {
         *      "Message": "Email example@gmail.com is already in use"
         *     }
         *
         */
        public IHttpActionResult Put([FromBody]Contact contact)
        {
            try
            {
                this.Factory.ContactsService.Update(contact);
                return Ok();
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(EntityValidationException ex)
            {
                return BadRequest(string.Join(",", ex.Validations.Select(x => x.ErrorMessage)));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        /**
         * @api {delete} /api/contacts Remove a contact
         * @apiName RemoveContact
         * @apiGroup Contacts
         * @apiVersion 1.0.0
         * @apiDescription  Remove a contact
         *
         * @apiParam {Integer} id Contact ID
         *
         * @apiSuccessExample Success-Response:
         *     HTTP/1.1 200 OK
         *
         *
         * @apiErrorExample  Error-Response:
         *     HTTP/1.1 400 Bad Request
         *     {
         *      "Message": "Invalid contact id"
         *     }
         *
         */
        public IHttpActionResult Delete(int id)
        {
            try
            {
                this.Factory.ContactsService.Remove(id);
                return Ok();
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}