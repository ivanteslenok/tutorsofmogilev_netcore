import {
  TUTOR,
  LOAD_LIST,
  CREATE_ITEM,
  UPDATE_ITEM,
  REMOVE_ITEM,
  START,
  SUCCESS,
  FAIL
} from '../constants';

const initialState = {
  items: [],
  totalCount: 0,
  loading: false,
  loaded: false,
  gridColumns: [
    { name: 'id', title: 'Id' },
    { name: 'firstName', title: 'Имя' },
    { name: 'lastName', title: 'Фамилия' },
    { name: 'education', title: 'Образование' },
    { name: 'job', title: 'Работа' },
    { name: 'address', title: 'Адресс' },
    { name: 'rating', title: 'Рейтинг' },
    { name: 'isVisible', title: 'Скрыть' }
  ]
};

export default (state = initialState, action) => {
  const { type, payload, data } = action;

  switch (type) {
    case TUTOR + LOAD_LIST + START:
    case TUTOR + CREATE_ITEM + START:
    case TUTOR + UPDATE_ITEM + START:
    case TUTOR + REMOVE_ITEM + START:
      return { ...state, loading: true };

    case TUTOR + LOAD_LIST + SUCCESS:
      return {
        ...state,
        items: data.items,
        totalCount: data.totalCount,
        loading: false,
        loaded: true
      };

    case TUTOR + CREATE_ITEM + SUCCESS:
      const itemsAfterCreate = [...state.items, data];
      return {
        ...state,
        items: itemsAfterCreate,
        totalCount: state.totalCount + 1,
        loading: false
      };

    case TUTOR + UPDATE_ITEM + SUCCESS:
      const itemsAfterUpdate = state.items.map(item =>
        item.id === payload.id ? { ...item, ...data } : item
      );
      return { ...state, items: itemsAfterUpdate, loading: false };

    case TUTOR + REMOVE_ITEM + SUCCESS:
      const itemsAfterRemove = state.items.filter(
        item => item.id !== payload.id
      );
      return { ...state, items: itemsAfterRemove, loading: false };

    case TUTOR + LOAD_LIST + FAIL:
    case TUTOR + CREATE_ITEM + FAIL:
    case TUTOR + UPDATE_ITEM + FAIL:
    case TUTOR + REMOVE_ITEM + FAIL:
      return { ...state, loading: false, loaded: false };

    default:
      return state;
  }
};
