import React, { useEffect, useState } from 'react';
import Button from '../../ui/Button';

const CategoriesPage = () => {
  const [categories, setCategories] = useState([]);

  const getAllCategories = async () => {
    try {
      const response = await fetch('https://localhost:7235/api/Category', {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });
      if (response.ok) {
        const data = await response.json();
        console.log(data);

        setCategories(data.categories);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  useEffect(() => {
    getAllCategories();
  }, []);

  return (
    <div className='max-w-7xl mx-auto mt-6'>
      <div className='text-center mb-4'>
        <h1 className='text-4xl font-bold mb-3'>Categorie di Esperienze</h1>
        <p className='text-gray-500 max-w-1/2 mx-auto'>
          Scopri il mondo attraverso le nostre esclusive categorie di
          esperienze, progettate per soddisfare ogni tipo di avventuriero.
        </p>
      </div>

      <div className='mb-4'>{/* elsenco esperienze */}</div>

      <div className='flex flex-col items-center text-center mb-4'>
        <h2 className='text-3xl font-bold mb-3'>Non sai da dove iniziare?</h2>
        <p className='text-gray-500 max-w-1/3 mb-3'>
          Sfoglia tutte le nostre esperienze e trova quella perfetta per te,
          indipendentemente dalla categoria.
        </p>
        <Button variant='primary'>Esplora tutte le esperienze</Button>
      </div>
    </div>
  );
};

export default CategoriesPage;
