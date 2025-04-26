import React from 'react';
import { loadStripe } from '@stripe/stripe-js';
import { Elements } from '@stripe/react-stripe-js';
import CheckoutForm from './CheckoutForm';

const stripePromise = loadStripe('pk_test_');

const StripeContainer = ({ sendOrder, orderAmount }) => {
  return (
    <Elements stripe={stripePromise}>
      <CheckoutForm sendOrder={sendOrder} orderAmount={orderAmount} />
    </Elements>
  );
};

export default StripeContainer;
