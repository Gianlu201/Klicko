import React from 'react';
import Button from '../components/ui/Button';
import { Link, useNavigate } from 'react-router-dom';
import { ShoppingCart, Trash2 } from 'lucide-react';
import { useDispatch, useSelector } from 'react-redux';
import { cartModified } from '../redux/actions';
import { toast } from 'sonner';

const CartPage = () => {
  const backendUrl = import.meta.env.VITE_BACKEND_URL;

  const cart = useSelector((state) => {
    return state.cart;
  });

  const fidelityCard = useSelector((state) => {
    return state.fidelityCard;
  });

  const dispatch = useDispatch();

  const navigate = useNavigate();

  const shippingPrice = fidelityCard?.points >= 1000 ? 0 : 4.99;

  const cartTotal = () => {
    let totalPrice = cart.experiences.reduce(
      (sum, element) => sum + element.price * element.quantity,
      0
    );

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
        `${backendUrl}/Cart/RemoveExperienceUnit/${cart.cartId}`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(experienceId),
        }
      );
      if (response.ok) {
        dispatch(cartModified());
      } else {
        throw new Error('Errore nella modifica del carrello!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
    }
  };

  const addExperienceUnit = async (experienceId) => {
    try {
      const response = await fetch(
        `${backendUrl}/Cart/AddExperienceUnit/${cart.cartId}`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(experienceId),
        }
      );
      if (response.ok) {
        dispatch(cartModified());
      } else {
        throw new Error('Errore nella modifica del carrello!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
    }
  };

  const removeExperienceFromCart = async (experienceId) => {
    try {
      const response = await fetch(
        `${backendUrl}/Cart/RemoveExperience/${cart.cartId}`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(experienceId),
        }
      );
      if (response.ok) {
        dispatch(cartModified());

        toast.success(
          <>
            <p className='font-bold'>Carrello modificato!</p>
            <p>Esperienza rimossa dal carrello!</p>
          </>
        );
      } else {
        throw new Error('Errore nella modifica del carrello!');
      }
    } catch (e) {
      toast.error(
        <>
          <p className='font-bold'>Errore!</p>
          <p>{e.message}</p>
        </>
      );
    }
  };

  return (
    <div className='max-w-7xl mx-auto min-h-screen px-6 xl:px-0'>
      <h1 className='text-4xl font-bold mt-10 mb-8'>Carrello</h1>
      {cart.experiences != undefined && cart.experiences.length > 0 ? (
        <div className='grid grid-cols-3 gap-10 mb-10'>
          {/* colonna tabella esperienze */}
          <div className='col-span-3 lg:col-span-2 bg-white rounded-2xl shadow-lg px-4 py-6'>
            <table className='w-full'>
              <thead>
                <tr className='grid grid-cols-24 gap-4 border-b border-gray-400/30 pb-3'>
                  <th className='hidden md:block col-span-3 text-gray-500 text-sm font-medium'>
                    Prodotto
                  </th>
                  <th className='col-span-12 md:col-span-10 text-gray-500 text-sm font-medium'>
                    Dettagli
                  </th>
                  <th className='col-span-5 text-gray-500 text-sm font-medium'>
                    Quantità
                  </th>
                  <th className='col-span-5 md:col-span-4 text-gray-500 text-sm font-medium'>
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
                    <td className='hidden md:block col-span-3 overflow-hidden'>
                      <div className='aspect-square rounded-lg overflow-hidden'>
                        <img
                          src={`https://localhost:7235/uploads/${exp.coverImage}`}
                          className='w-full h-full object-cover'
                        />
                      </div>
                    </td>

                    <td className='col-span-11 md:col-span-10'>
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

                    <td className='col-span-5 grid grid-cols-4 justify-center items-center gap-2'>
                      <Button
                        variant='outline'
                        size='sm'
                        className='order-2 md:order-1 col-span-2 md:col-span-1 text-xl font-bold w-8 h-8'
                        onClick={() => {
                          removeExperienceUnit(exp.experienceId);
                        }}
                      >
                        -
                      </Button>

                      <span className='order-1 md:order-2 col-span-4 md:col-span-2 flex justify-center items-center bg-background border border-gray-400/30 rounded-xl h-8'>
                        {exp.quantity}
                      </span>

                      <Button
                        variant='outline'
                        size='sm'
                        className='order-3 md:order-3 col-span-2 md:col-span-1 text-xl font-bold w-8 h-8'
                        onClick={() => {
                          addExperienceUnit(exp.experienceId);
                        }}
                      >
                        +
                      </Button>
                    </td>

                    <td className='col-span-6 md:col-span-4'>
                      <p className='text-sm md:text-lg font-semibold text-center mb-1'>
                        {(exp.price * (1 - exp.sale / 100) * exp.quantity)
                          .toFixed(2)
                          .replace('.', ',')}{' '}
                        €
                      </p>
                      <p className='text-gray-500 text-xs sm:text-sm font-normal text-center'>
                        {exp.quantity} x{' '}
                        {exp.price.toFixed(2).replace('.', ',')} €
                        {exp.sale > 0 && (
                          <span className='block text-gray-500 text-xs sm:text-sm font-normal ms-2 mt-1'>
                            -{exp.sale}%
                          </span>
                        )}
                      </p>
                    </td>

                    <td className='col-span-2 pe-1'>
                      <Trash2
                        className='text-red-500 w-4 h-4 ms-auto cursor-pointer'
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
          <div className='col-span-3 lg:col-span-1 bg-white rounded-2xl shadow-lg px-4 py-6 h-fit'>
            <h3 className='text-xl font-semibold mb-4'>Riepilogo ordine</h3>
            <div className='flex justify-between items-center text-gray-600 my-3'>
              <span>Subtotale ({cart.experiences.length} esperienze)</span>
              <span>{cartTotal().toFixed(2).replace('.', ',')} €</span>
            </div>
            {cartTotalExperienceDiscount() > 0 && (
              <div className='flex justify-between items-center text-gray-600 my-3'>
                <span>Totale sconti</span>
                <span>
                  -{cartTotalExperienceDiscount().toFixed(2).replace('.', ',')}{' '}
                  €
                </span>
              </div>
            )}

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
            <p className='text-gray-500 mb-5 px-26 text-center'>
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
