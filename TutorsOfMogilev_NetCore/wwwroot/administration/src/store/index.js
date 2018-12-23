import { createStore, applyMiddleware } from 'redux';
// import thunk from 'redux-thunk';
import get from '../middlewares/get';
import post from '../middlewares/post';
import put from '../middlewares/put';
import del from '../middlewares/del';
import reducer from '../reducer';

const enhancer = applyMiddleware(get, post, put, del);
const store = createStore(reducer, {}, enhancer);

// console.dir(store.getState());

export default store;
