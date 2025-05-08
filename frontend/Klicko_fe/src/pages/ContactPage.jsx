import { Mail, MapPin, Phone } from 'lucide-react';
import React from 'react';

const ContactPage = () => {
  return (
    <div className='max-w-5xl min-h-screen px-6 mx-auto mt-6 mb-8 xl:px-0'>
      <h1 className='mb-3 text-3xl font-bold'>Contattaci</h1>

      <div className='grid grid-cols-2 gap-6 md:grid-cols-3'>
        <div className='px-6 py-5 bg-white border rounded-lg shadow border-gray-400/30'>
          <h3 className='flex justify-start items-center gap-1.5 text-xl font-semibold mb-3'>
            <Mail className='w-5 h-5 text-primary' />
            Email
          </h3>
          <a
            href='mailto:info@klicko.com'
            className='text-primary hover:underline'
          >
            info@klicko.com
          </a>
        </div>

        <div className='px-6 py-5 bg-white border rounded-lg shadow border-gray-400/30'>
          <h3 className='flex justify-start items-center gap-1.5 text-xl font-semibold mb-3'>
            <Phone className='w-5 h-5 text-primary' />
            Telefono
          </h3>
          <a href='tel:+390123456789' className='text-primary hover:underline'>
            +39 012 345 6789
          </a>
        </div>

        <div className='px-6 py-5 bg-white border rounded-lg shadow border-gray-400/30'>
          <h3 className='flex justify-start items-center gap-1.5 text-xl font-semibold mb-3'>
            <MapPin className='w-5 h-5 text-primary' />
            Sede
          </h3>
          <p className='mb-1 text-gray-500'>Via Piave, 123</p>
          <p className='mb-1 text-gray-500'>00187 Roma (RM)</p>
          <p className='text-gray-500'>Italia</p>
        </div>
      </div>
    </div>
  );
};

export default ContactPage;
