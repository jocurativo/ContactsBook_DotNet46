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
        public IEnumerable<ContactInfoDto> Get()
        {
            return this.Factory.ContactsService.GetFiltered(string.Empty);
        }

        public IHttpActionResult Get(int id)
        {
            var contact = this.Factory.ContactsService.GetById(id);

            if (contact == null) 
                return NotFound() ;
            
            return Ok(contact);
        }

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
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest();
            }
        }

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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest();
            }
        }

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