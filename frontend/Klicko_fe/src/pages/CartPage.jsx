import React from 'react';
import Button from '../components/ui/Button';
import { Link, useNavigate } from 'react-router-dom';
import { ShoppingCart, Trash2 } from 'lucide-react';
import { useDispatch, useSelector } from 'react-redux';
import { cartModified } from '../redux/actions';

const CartPage = () => {
  const cart = useSelector((state) => {
    return state.cart;
  });

  const dispatch = useDispatch();

  const navigate = useNavigate();

  const shippingPrice = 4.99;

  const cartTotal = () => {
    let totalPrice = cart.experiences.reduce(
      (sum, element) => sum + element.price * element.quantity,
      0
    );

    // console.log(totalPrice);

    return totalPrice;
  };

  const cartTotalExperienceDiscount = () => {
    let totalDiscount = cart.experiences.reduce(
      (sum, element) =>
        sum + element.price * (element.sale / 100) * element.quantity,
      0
    );

    return totalDiscount;
  };

  const removeExperienceUnit = async (experienceId) => {
    try {
      const response = await fetch(
        `https://localhost:7235/api/Cart/RemoveExperienceUnit/${cart.cartId}`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(experienceId),
        }
      );
      if (response.ok) {
        const data = await response.json();
        dispatch(cartModified());
        // console.log(data.cart);

        console.log(data);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  const addExperienceUnit = async (experienceId) => {
    try {
      const response = await fetch(
        `https://localhost:7235/api/Cart/AddExperienceUnit/${cart.cartId}`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(experienceId),
        }
      );
      if (response.ok) {
        const data = await response.json();
        dispatch(cartModified());
        // console.log(data.cart);

        console.log(data);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  const removeExperienceFromCart = async (experienceId) => {
    try {
      const response = await fetch(
        `https://localhost:7235/api/Cart/RemoveExperience/${cart.cartId}`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(experienceId),
        }
      );
      if (response.ok) {
        const data = await response.json();
        dispatch(cartModified());
        // console.log(data.cart);

        console.log(data);
      } else {
        throw new Error('Errore nel recupero dei dati!');
      }
    } catch {
      console.log('Error');
    }
  };

  return (
    <div className='max-w-7xl mx-auto min-h-screen'>
      <h1 className='text-4xl font-bold mt-10 mb-8'>Carrello</h1>
      {cart.experiences != undefined && cart.experiences.length > 0 ? (
        <div className='grid grid-cols-3 gap-10'>
          {/* colonna tabella esperienze */}
          <div className='col-span-2 bg-white rounded-2xl shadow-lg px-4 py-6'>
            <table className='w-full'>
              <thead>
                <tr className='grid grid-cols-24 gap-4 border-b border-gray-400/30 pb-3'>
                  <th className='col-span-3 text-gray-500 text-sm font-medium'>
                    Prodotto
                  </th>
                  <th className='col-span-10 text-gray-500 text-sm font-medium'>
                    Dettagli
                  </th>
                  <th className='col-span-5 text-gray-500 text-sm font-medium'>
                    Quantità
                  </th>
                  <th className='col-span-4 text-gray-500 text-sm font-medium'>
                    Prezzo
                  </th>
                  <th className='col-span-2'></th>
                </tr>
              </thead>
              <tbody>
                {cart.experiences.map((exp) => (
                  <tr
                    key={exp.experienceId}
                    className='grid grid-cols-24 gap-4 items-center hover:bg-gray-100 border-b border-gray-400/30 py-3 px-2 last-of-type:border-0'
                  >
                    <td className='col-span-3 overflow-hidden'>
                      <div className='aspect-square rounded-lg overflow-hidden'>
                        <img
                          src={`https://localhost:7235/uploads/${exp.coverImage}`}
                          className='w-full h-full object-cover'
                        />
                      </div>
                    </td>

                    <td className='col-span-10'>
                      <Link
                        to={`/experiences/detail/${exp.experienceId}`}
                        className='text-sm hover:text-primary'
                      >
                        <h6>{exp.title}</h6>
                      </Link>
                      <p className='text-gray-500 text-sm font-normal my-0.5'>
                        {exp.place}
                      </p>
                      <p className='text-gray-500 text-sm font-normal'>
                        {exp.duration}
                      </p>
                    </td>

                    <td className='col-span-5 flex justify-center items-center gap-2'>
                      <Button
                        variant='outline'
                        size='sm'
                        className='text-xl font-bold w-8 h-8'
                        onClick={() => {
                          removeExperienceUnit(exp.experienceId);
                        }}
                      >
                        -
                      </Button>

                      <span className='flex items-center bg-background px-6 border border-gray-400/30 rounded-xl h-8'>
                        {exp.quantity}
                      </span>

                      <Button
                        variant='outline'
                        size='sm'
                        className='text-xl font-bold w-8 h-8'
                        onClick={() => {
                          addExperienceUnit(exp.experienceId);
                        }}
                      >
                        +
                      </Button>
                    </td>

                    <td className='col-span-4'>
                      <p className='text-lg font-semibold text-center'>
                        {(exp.price * (1 - exp.sale / 100) * exp.quantity)
                          .toFixed(2)
                          .replace('.', ',')}{' '}
                        €
                      </p>
                      <p className='text-gray-500 text-sm font-normal text-center'>
                        {exp.quantity} x{' '}
                        {exp.price.toFixed(2).replace('.', ',')} €
                        {exp.sale > 0 && (
                          <span className='text-gray-500 text-sm font-normal ms-2'>
                            -{exp.sale}%
                          </span>
                        )}
                      </p>
                    </td>

                    <td className='col-span-2'>
                      <Trash2
                        className='text-red-500 w-4 h-4 ms-auto me-4 cursor-pointer'
                        onClick={() => {
                          removeExperienceFromCart(exp.experienceId);
                        }}
                      />
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          {/* colonna resoconto */}
          <div className='bg-white rounded-2xl shadow-lg px-4 py-6 h-fit'>
            <h3 className='text-xl font-semibold mb-4'>Riepilogo ordine</h3>
            <div className='flex justify-between items-center text-gray-600 my-3'>
              <span>Subtotale ({cart.experiences.length} esperienze)</span>
              <span>{cartTotal().toFixed(2).replace('.', ',')} €</span>
            </div>
            <div className='flex justify-between items-center text-gray-600 my-3'>
              <span>Totale sconti</span>
              <span>
                {cartTotalExperienceDiscount().toFixed(2).replace('.', ',')} €
              </span>
            </div>
            <div className='flex justify-between items-center text-gray-600 my-3'>
              <span>Costo di spedizione</span>
              <span>{shippingPrice.toFixed(2).replace('.', ',')} €</span>
            </div>

            <hr className='text-gray-400/30 my-3' />

            <div className='flex justify-between items-center text-gray-600 font-semibold my-5'>
              <span>Totale</span>
              <span>
                {(cartTotal() - cartTotalExperienceDiscount() + shippingPrice)
                  .toFixed(2)
                  .replace('.', ',')}{' '}
                €
              </span>
            </div>

            <Button
              variant='primary'
              fullWidth={true}
              className='mb-3'
              onClick={() => {
                navigate('/checkout');
              }}
            >
              Procedi al checkout
            </Button>

            <Link
              to='/experiences'
              className='text-primary text-sm hover:underline'
            >
              Continua lo shopping
            </Link>
          </div>
        </div>
      ) : (
        <div>
          <div className='flex flex-col min-h-[60vh] justify-center items-center bg-white rounded-2xl shadow-lg'>
            <ShoppingCart className='w-20 h-20 text-gray-400/60 mb-3' />
            <h3 className='text-xl font-semibold mb-3'>
              Il tuo carrello è vuoto
            </h3>
            <p className='text-gray-500 mb-5'>
              Non hai ancora aggiunto esperienze al tuo carrello
            </p>
            <Button
              variant='primary'
              onClick={() => {
                navigate('/experiences');
              }}
            >
              Sfoglia le esperienze
            </Button>
          </div>
        </div>
      )}
    </div>
  );
};

export default CartPage;
