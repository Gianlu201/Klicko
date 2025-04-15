import React, { useEffect, useState } from 'react';
import Button from '../ui/Button';
import { Funnel, Pencil, Plus, Search, Trash2 } from 'lucide-react';
import ToggleSwitch from '../ui/ToggleSwitch';
import { useNavigate } from 'react-router-dom';
import UploadFile from '../ui/UploadFile';

const AddExperienceForm = () => {
  const [title, setTitle] = useState('');
  const [categoryId, setCategoryId] = useState('');
  const [descriptionShort, setDescriptionShort] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState(0);
  const [place, setPlace] = useState('');
  const [duration, setDuration] = useState('');
  const [maxParticipants, setMaxParticipants] = useState(1);
  const [organiser, setOrganiser] = useState('');
  const [sale, setSale] = useState(0);
  const [validityInMonths, setValidityInMonths] = useState(1);
  const [includedDescription, setIncludedDescription] = useState('');
  const [isFreeCancellable, setIsFreeCancellable] = useState(true);
  const [isInEvidence, setIsInEvidence] = useState(false);
  const [isPopular, setIsPopular] = useState(false);
  const [coverImage, setCoverImage] = useState(null);
  const [carryWith, setCarryWith] = useState('');
  const [images, setImages] = useState(null);

  const [categories, setCategories] = useState([]);

  const navigate = useNavigate();

  // const getAllExperiences = async () => {
  //   try {
  //     const response = await fetch('https://localhost:7235/api/Experience', {
  //       method: 'GET',
  //       headers: {
  //         'Content-Type': 'application/json',
  //       },
  //     });
  //     if (response.ok) {
  //       const data = await response.json();

  //       console.log(data);

  //       setExperiences(data.experiences);
  //     } else {
  //       throw new Error('Errore nel recupero dei dati!');
  //     }
  //   } catch {
  //     console.log('Error');
  //   }
  // };

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

        // console.log(data);

        setCategories(data.categories);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  // const sendForm = async () => {
  //   try {
  //     const body = {
  //       title: title,
  //       categoryId: categoryId,
  //       duration: duration,
  //       place: place,
  //       price: price,
  //       descriptionShort: descriptionShort,
  //       description: description,
  //       maxParticipants: maxParticipants,
  //       organiser: organiser,
  //       isFreeCancellable: isFreeCancellable,
  //       includedDescription: includedDescription,
  //       sale: sale,
  //       isInEvidence: isInEvidence,
  //       isPopular: isPopular,
  //       validityInMonths: validityInMonths,
  //       coverImage: null,
  //     };

  //     // formData.append('title', title);
  //     // formData.append('categoryId', categoryId);
  //     // formData.append('duration', duration);
  //     // formData.append('place', place);
  //     // formData.append('price', price);
  //     // formData.append('descriptionShort', descriptionShort);
  //     // formData.append('description', description);
  //     // formData.append('maxParticipants', maxParticipants);
  //     // formData.append('organiser', organiser);
  //     // formData.append('isFreeCancellable', isFreeCancellable);
  //     // formData.append('includedDescription', includedDescription);
  //     // formData.append('sale', sale);
  //     // formData.append('isInEvidence', isInEvidence);
  //     // formData.append('isPopular', isPopular);
  //     // formData.append('validityInMonths', validityInMonths);
  //     // formData.append('coverImage', coverImage);
  //     // formData.append('images', null);
  //     // formData.append('carryWiths', null);

  //     console.log(body);

  //     const response = await fetch('https://localhost:7235/api/Experience', {
  //       method: 'POST',
  //       headers: {
  //         'Content-Type': 'application/json',
  //       },
  //       body: JSON.stringify(body),
  //     });
  //     if (response.ok) {
  //       const data = await response.json();

  //       // console.log(data);

  //       setCategories(data.categories);
  //     } else {
  //       throw new Error('Errore nel recupero dei dati!');
  //     }
  //   } catch {
  //     console.log('Error');
  //   }
  // };

  const sendForm = async () => {
    try {
      let tokenObj = localStorage.getItem('klicko_token');

      if (!tokenObj) {
        navigate('/login');
      }

      let token = JSON.parse(tokenObj).token;

      const formData = new FormData();

      formData.append('Title', title);
      formData.append('CategoryId', categoryId);
      formData.append('Duration', duration);
      formData.append('Place', place);
      formData.append('Price', price);
      formData.append('DescriptionShort', descriptionShort);
      formData.append('Description', description);
      formData.append('MaxParticipants', maxParticipants);
      formData.append('Organiser', organiser);
      formData.append('IsFreeCancellable', isFreeCancellable);
      formData.append('IncludedDescription', includedDescription);
      formData.append('Sale', sale);
      formData.append('IsInEvidence', isInEvidence);
      formData.append('IsPopular', isPopular);
      formData.append('ValidityInMonths', validityInMonths);

      if (carryWith.length > 0) {
        const list = carryWith.split(',');

        const carryList = [];
        list.forEach((element) => {
          carryList.push(element);
        });

        if (carryList.length > 0) {
          console.log(carryList);
          formData.append('CarryWiths', carryList);
        }
      }

      if (coverImage) {
        formData.append('CoverImage', coverImage); // file immagine
      }

      if (images && images.length > 0) {
        for (let i = 0; i < images.length; i++) {
          formData.append('Images', images[i]);
        }
      }

      const response = await fetch('https://localhost:7235/api/Experience', {
        method: 'POST',
        headers: {
          Authorization: `Bearer ${token}`,
        },

        body: formData,
      });

      if (response.ok) {
        const data = await response.json();
        console.log('Upload riuscito:', data);
      } else {
        throw new Error('Errore nel salvataggio!');
      }
    } catch (err) {
      console.error('Errore:', err);
    }
  };

  useEffect(() => {
    getAllCategories();
  }, []);

  return (
    <>
      <div className='flex flex-col justify-start items-start mb-6'>
        <h2 className='text-2xl font-bold mb-2'>Aggiungi esperienza</h2>
        <p className='text-gray-500 font-normal'>
          Crea una nuova esperienza da offrire ai clienti
        </p>
      </div>

      <form>
        <div className='grid grid-cols-2 gap-8 mb-8'>
          {/* titolo */}
          <div className='flex flex-col justify-start items-start gap-2'>
            <label htmlFor='title' className='font-semibold text-sm'>
              Titolo
            </label>
            <input
              type='text'
              id='title'
              className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3 w-full'
              placeholder='Inserisci un titolo accattivante'
              value={title}
              onChange={(e) => {
                setTitle(e.target.value);
              }}
            />
            <span className='text-gray-500 text-sm'>
              Questo sarà il titolo principale dell'esperienza
            </span>
          </div>

          {/* categoria */}
          <div className='flex flex-col justify-start items-start gap-2'>
            <label htmlFor='category' className='font-semibold text-sm'>
              Categoria
            </label>
            <select
              id='category'
              className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3 w-full'
              value={categoryId}
              onChange={(e) => {
                setCategoryId(e.target.value);
              }}
            >
              <option value=''>Seleziona una categoria</option>
              {categories.map((category) => (
                <option value={category.categoryId} key={category.categoryId}>
                  {category.name}
                </option>
              ))}
            </select>
            <span className='text-gray-500 text-sm'>
              La categoria aiuta i clienti a trovare la tua esperienza
            </span>
          </div>
        </div>

        {/* descrizione breve */}
        <div className='flex flex-col justify-start items-start gap-2 mb-8'>
          <label htmlFor='descriptionShort' className='font-semibold text-sm'>
            Descrizione breve
          </label>
          <textarea
            type='text'
            id='descriptionShort'
            className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3 w-full'
            placeholder={`Una breve descrizione dell'esperienza`}
            value={descriptionShort}
            onChange={(e) => {
              setDescriptionShort(e.target.value);
            }}
          ></textarea>
          <span className='text-gray-500 text-sm'>
            Questa verrà mostrata nelle anteprime delle card
          </span>
        </div>

        {/* descrizione completa */}
        <div className='flex flex-col justify-start items-start gap-2 mb-8'>
          <label htmlFor='description' className='font-semibold text-sm'>
            Descrizione completa
          </label>
          <textarea
            type='text'
            id='description'
            className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3 w-full'
            placeholder={`Descrivi in dettaglio cosa include questa esperienza`}
            value={description}
            onChange={(e) => {
              setDescription(e.target.value);
            }}
          ></textarea>
          <span className='text-gray-500 text-sm'>
            Descrivi ogni aspetto dell'esperienza: cosa include, cosa
            aspettarsi, ecc.
          </span>
        </div>

        <div className='grid grid-cols-3 gap-8 mb-8'>
          {/* organizzatore */}
          <div className='flex flex-col justify-start items-start gap-2'>
            <label htmlFor='organiser' className='font-semibold text-sm'>
              Organizzatore
            </label>
            <input
              type='text'
              id='organiser'
              className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3 w-full'
              placeholder='Es: Italia Avventure'
              value={organiser}
              onChange={(e) => {
                setOrganiser(e.target.value);
              }}
            />
          </div>

          {/* località */}
          <div className='flex flex-col justify-start items-start gap-2'>
            <label htmlFor='place' className='font-semibold text-sm'>
              Località
            </label>
            <input
              type='text'
              id='place'
              className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3 w-full'
              placeholder='Es: Roma, Lazio'
              value={place}
              onChange={(e) => {
                setPlace(e.target.value);
              }}
            />
          </div>

          {/* durata */}
          <div className='flex flex-col justify-start items-start gap-2'>
            <label htmlFor='duration' className='font-semibold text-sm'>
              Durata
            </label>
            <input
              type='text'
              id='duration'
              className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3 w-full'
              placeholder='Es: 2 ore, 3 giorni'
              value={duration}
              onChange={(e) => {
                setDuration(e.target.value);
              }}
            />
          </div>
        </div>

        <div className='grid grid-cols-4 gap-8 mb-8'>
          {/* prezzo */}
          <div className='flex flex-col justify-start items-start gap-2'>
            <label htmlFor='price' className='font-semibold text-sm'>
              Prezzo
            </label>
            <input
              type='number'
              min={0}
              step={0.01}
              id='price'
              className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3 w-full'
              placeholder='0'
              value={price}
              onChange={(e) => {
                setPrice(e.target.value);
              }}
            />
          </div>

          {/* massimo di partecipanti */}
          <div className='flex flex-col justify-start items-start gap-2'>
            <label htmlFor='maxParticipants' className='font-semibold text-sm'>
              Massimo di partecipanti
            </label>
            <input
              type='number'
              min={1}
              step={1}
              id='maxParticipants'
              className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3 w-full'
              placeholder='1'
              value={maxParticipants}
              onChange={(e) => {
                setMaxParticipants(e.target.value);
              }}
            />
          </div>

          {/* sconto */}
          <div className='flex flex-col justify-start items-start gap-2'>
            <label htmlFor='sale' className='font-semibold text-sm'>
              Sconto
            </label>
            <input
              type='number'
              min={0}
              step={1}
              id='sale'
              className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3 w-full'
              placeholder='0'
              value={sale}
              onChange={(e) => {
                setSale(e.target.value);
              }}
            />
          </div>

          {/* validità in mesi */}
          <div className='flex flex-col justify-start items-start gap-2'>
            <label htmlFor='sale' className='font-semibold text-sm'>
              Validità in mesi
            </label>
            <input
              type='number'
              min={1}
              step={1}
              id='validity'
              className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3 w-full'
              placeholder='0'
              value={validityInMonths}
              onChange={(e) => {
                setValidityInMonths(e.target.value);
              }}
            />
          </div>
        </div>

        {/* cosa è incluso */}
        <div className='flex flex-col justify-start items-start gap-2 mb-8'>
          <label htmlFor='included' className='font-semibold text-sm'>
            Cosa è incluso:
          </label>
          <textarea
            type='text'
            id='included'
            className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3 w-full'
            placeholder={`Una breve descrizione di cosa include l'esperienza`}
            value={includedDescription}
            onChange={(e) => {
              setIncludedDescription(e.target.value);
            }}
          ></textarea>
          <span className='text-gray-500 text-sm'>
            Indica cosa include l'esperienza
          </span>
        </div>

        <div className='grid grid-cols-2 gap-8 mb-8'>
          {/* in evidenza */}
          <div className='flex justify-between items-center gap-3 border border-gray-400/30 rounded-lg p-3'>
            <div className='w-[calc(100%-50px)] flex flex-col justify-center items-start gap-2'>
              <span className='font-semibold text-sm'>In evidenza</span>
              <span className='text-sm text-gray-500'>
                Mostra questa esperienza nella sezione in evidenza della
                homepage
              </span>
            </div>
            <div className='w-[50px]'>
              <ToggleSwitch
                value={isInEvidence}
                onClick={() => {
                  setIsInEvidence(!isInEvidence);
                }}
              />
            </div>
          </div>

          {/* popolare */}
          <div className='flex justify-between items-center gap-3 border border-gray-400/30 rounded-lg p-3'>
            <div className='w-[calc(100%-50px)] flex flex-col justify-center items-start gap-2'>
              <span className='font-semibold text-sm'>Popolare</span>
              <span className='text-sm text-gray-500'>
                Mostra questa esperienza nella sezione esperienze popolari
              </span>
            </div>
            <div className='w-[50px]'>
              <ToggleSwitch
                value={isPopular}
                onClick={() => {
                  setIsPopular(!isPopular);
                }}
              />
            </div>
          </div>

          {/* cancellazione gratuita */}
          <div className='flex justify-between items-center gap-3 border border-gray-400/30 rounded-lg p-3'>
            <div className='w-[calc(100%-50px)] flex flex-col justify-center items-start gap-2'>
              <span className='font-semibold text-sm'>
                Cancellazione gratuita
              </span>
              <span className='text-sm text-gray-500'>
                L'esperienza consente la cancellazione gratuita fino a 48h prima
              </span>
            </div>
            <div className='w-[50px]'>
              <ToggleSwitch
                value={isFreeCancellable}
                onClick={() => {
                  setIsFreeCancellable(!isFreeCancellable);
                }}
              />
            </div>
          </div>
        </div>

        {/* immagine copertina */}
        <div className='flex flex-col justify-start items-start gap-2 mb-8'>
          <label className='font-semibold text-sm'>Immagine copertina</label>

          <UploadFile
            multiple={false}
            onFilesSelected={(files) => setCoverImage(files)}
          />
          {/* <input
            type='file'
            accept='image/*'
            id='coverImg'
            className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3'
            onChange={(e) => {
              setCoverImage(e.target.files[0]);
            }}
          /> */}
          <span className='text-gray-500 text-sm'>
            Questa immagine verrà mostrata nell'anteprima dell'esperienza
          </span>
        </div>

        {/* cosa portare */}
        <div className='flex flex-col justify-start items-start gap-2 mb-8'>
          <label htmlFor='carryWithInput' className='font-semibold text-sm'>
            Cosa portare
          </label>
          <input
            type='text'
            id='carryWithInput'
            className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3 w-full'
            placeholder='Es: Scarpe comode, Ombrello, Acqua'
            value={carryWith}
            onChange={(e) => {
              setCarryWith(e.target.value);
            }}
          />
          <span className='text-gray-500 text-sm'>
            Inserire le cose da portare separate tra loro da una virgola
          </span>
        </div>

        {/* elenco immagini multiple */}
        {/* <div className='flex flex-col justify-start items-start gap-2 mb-8'>
          <label htmlFor='otherimages' className='font-semibold text-sm'>
            Immagini aggiuntive
          </label>
          <input
            type='file'
            multiple
            accept='image/*'
            id='otherimages'
            className='text-sm bg-background border border-gray-400/30 rounded-lg py-2 px-3'
            onChange={(e) => {
              setImages([...e.target.files]);
            }}
          />
          <span className='text-gray-500 text-sm'>
            Questa immagine verrà mostrata nei dettagli dell'esperienza
          </span>
        </div> */}

        {/* elenco immagini multiple */}
        <div className='flex flex-col justify-start items-start gap-2 my-8'>
          <label className='font-semibold text-sm'>Immagini aggiuntive</label>
          <UploadFile
            multiple={true}
            onFilesSelected={(files) => setImages(files)}
          />
          <span className='text-gray-500 text-sm'>
            Questa immagine verrà mostrata nei dettagli dell'esperienza
          </span>
        </div>

        <div className='flex justify-start items-center gap-3'>
          <Button
            variant='outline'
            onClick={() => {
              navigate('/dashboard/experiences');
            }}
          >
            Annulla
          </Button>
          <Button
            variant='primary'
            onClick={() => {
              sendForm();
            }}
          >
            Crea esperienza
          </Button>
        </div>
      </form>
    </>
  );
};

export default AddExperienceForm;
