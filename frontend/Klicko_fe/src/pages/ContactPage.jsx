import { Mail, MapPin, Phone } from 'lucide-react';
import React from 'react';

const ContactPage = () => {
  return (
    <div className='max-w-5xl mx-auto mb-8 mt-6 min-h-screen'>
      <h1 className='text-3xl font-bold mb-3'>Contattaci</h1>

      <div className='grid grid-cols-3 gap-6'>
        <div className='bg-white border border-gray-400/30 rounded-lg shadow px-6 py-5'>
          <h3 className='flex justify-start items-center gap-1.5 text-xl font-semibold mb-3'>
            <Mail className='text-primary w-5 h-5' />
            Email
          </h3>
          <a
            href='mailto:info@klicko.com'
            className='text-primary hover:underline'
          >
            info@klicko.com
          </a>
        </div>

        <div className='bg-white border border-gray-400/30 rounded-lg shadow px-6 py-5'>
          <h3 className='flex justify-start items-center gap-1.5 text-xl font-semibold mb-3'>
            <Phone className='text-primary w-5 h-5' />
            Telefono
          </h3>
          <a href='tel:+390123456789' className='text-primary hover:underline'>
            +39 012 345 6789
          </a>
        </div>

        <div className='bg-white border border-gray-400/30 rounded-lg shadow px-6 py-5'>
          <h3 className='flex justify-start items-center gap-1.5 text-xl font-semibold mb-3'>
            <MapPin className='text-primary w-5 h-5' />
            Sede
          </h3>
          <p className='text-gray-500 mb-1'>Via Piave, 123</p>
          <p className='text-gray-500 mb-1'>00187 Roma (RM)</p>
          <p className='text-gray-500'>Italia</p>
        </div>
      </div>
    </div>
  );
};

export default ContactPage;
