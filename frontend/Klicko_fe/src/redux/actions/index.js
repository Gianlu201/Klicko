import { jwtDecode } from 'jwt-decode';
import { toast } from 'sonner';

export const SET_LOGGED_USER = 'SET_LOGGED_USER';
export const LOGOUT = 'LOGOUT';
export const SET_USER_CART = 'SET_USER_CART';
export const EMPTY_CART = 'EMPTY_CART';
export const CART_MODIFIED = 'CART_MODIFIED';
export const SET_CART_FROM_LOCAL = 'SET_CART_FROM_LOCAL';
export const ADD_EXPERIENCE_TO_LOCAL_CART = 'ADD_EXPERIENCE_TO_LOCAL_CART';
export const REMOVE_EXPERIENCE_FROM_LOCAL_CART =
  'REMOVE_EXPERIENCE_FROM_LOCAL_CART';
export const ADD_EXPERIENCE_UNIT_TO_LOCAL_CART =
  'ADD_EXPERIENCE_UNIT_TO_LOCAL_CART';
export const REMOVE_EXPERIENCE_UNIT_FROM_LOCAL_CART =
  'REMOVE_EXPERIENCE_UNIT_FROM_LOCAL_CART';
export const SET_SEARCHBAR_QUERY = 'SET_SEARCHBAR_QUERY';
export const SET_SELECTED_CATEGORY = 'SET_SELECTED_CATEGORY';
export const SET_USER_FIDELITY_CARD = 'SET_USER_FIDELITY_CARD';

export const setLoggedUser = (data) => {
  const tokenDecoded = jwtDecode(data.token);

  const userInfos = {
    aud: tokenDecoded.aud,
    exp: tokenDecoded.exp,
    // role: tokenDecoded[
    //   'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    // ],
    role: tokenDecoded.role,
    email: tokenDecoded.email,
    name: tokenDecoded.name,
    surname: tokenDecoded.surname,
    id: tokenDecoded.nameidentifier,
    cartId: tokenDecoded.cartId,
    fidelityCardId: tokenDecoded.fidelityCardId,
    iss: tokenDecoded.iss,
    expiration: data.expires,
  };

  toast.success(`Bentornato ${userInfos.name}`);

  return {
    type: SET_LOGGED_USER,
    payload: userInfos,
  };
};

export const logoutUser = () => {
  localStorage.removeItem('klicko_token');
  return {
    type: LOGOUT,
  };
};

export const setUserCart = (cart) => {
  const experiences = [];

  if (cart.cartExperiences !== null) {
    cart.cartExperiences.forEach((exp) => {
      experiences.push({
        title: exp.title,
        categoryName: exp.category.name,
        coverImage: exp.coverImage,
        experienceId: exp.experienceId,
        isFreeCancellable: exp.isFreeCancellable,
        place: exp.place,
        duration: exp.duration,
        price: exp.price,
        quantity: exp.quantity,
        sale: exp.sale,
      });
    });
  }

  const userCart = {
    cartId: cart.cartId,
    createdAt: cart.createdAt,
    updatedAt: cart.updatedAt,
    experiences: experiences,
    modified: false,
  };

  return {
    type: SET_USER_CART,
    payload: userCart,
  };
};

export const emptyCart = () => ({
  type: EMPTY_CART,
});

export const cartModified = () => ({
  type: CART_MODIFIED,
});

export const setCartFromLocal = (localCart) => ({
  type: SET_CART_FROM_LOCAL,
  payload: localCart,
});

export const addExperienceToLocalCart = (experience) => {
  let newCartExperience = {
    title: experience.title,
    categoryName: experience.category.name,
    coverImage: experience.coverImage,
    experienceId: experience.experienceId,
    isFreeCancellable: experience.isFreeCancellable,
    place: experience.place,
    duration: experience.duration,
    price: experience.price,
    quantity: 1,
    sale: experience.sale,
  };

  return {
    type: ADD_EXPERIENCE_TO_LOCAL_CART,
    payload: newCartExperience,
  };
};

export const removeExperienceFromLocalCart = (experienceId) => ({
  type: REMOVE_EXPERIENCE_FROM_LOCAL_CART,
  payload: experienceId,
});

export const addExperienceUnitToLocalCart = (experienceId) => ({
  type: ADD_EXPERIENCE_UNIT_TO_LOCAL_CART,
  payload: experienceId,
});

export const removeExperienceUnitFromLocalCart = (experienceId) => ({
  type: REMOVE_EXPERIENCE_UNIT_FROM_LOCAL_CART,
  payload: experienceId,
});

export const setUserFidelityCard = (fidelityCard) => {
  const userCard = {
    fidelityCardId: fidelityCard.fidelityCardId,
    cardNumber: fidelityCard.cardNumber,
    points: fidelityCard.points,
    availablePoints: fidelityCard.availablePoints,
  };

  return {
    type: SET_USER_FIDELITY_CARD,
    payload: userCard,
  };
};

export const setSearchBarQuery = (query) => ({
  type: SET_SEARCHBAR_QUERY,
  payload: query,
});

export const setSelectedCategoryName = (categoryName) => ({
  type: SET_SELECTED_CATEGORY,
  payload: categoryName,
});
