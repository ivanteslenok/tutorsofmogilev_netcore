import moment from 'moment'

import {
  TUTOR,
  PHONE,
  CONTACT,
  LOAD_LIST,
  SAVE_QUERY_PARAMS,
  CREATE_ITEM,
  UPDATE_ITEM,
  REMOVE_ITEM,
  UPDATE_TUTORS_SUBJECTS,
  UPDATE_TUTORS_SPECIALIZATIONS,
  START,
  SUCCESS,
  FAIL
} from '../constants';

const gridSettings = [
  {
    name: 'firstName',
    title: 'Имя',
    width: 180,
    getCellValue: row => row.firstName
  },
  {
    name: 'lastName',
    title: 'Фамилия',
    width: 180,
    getCellValue: row => row.lastName
  },
  {
    name: 'patronymic',
    title: 'Отчество',
    width: 180,
    getCellValue: row => row.patronymic
  },
  {
    name: 'education',
    title: 'Образование',
    width: 240,
    getCellValue: row => row.education
  },
  {
    name: 'job',
    title: 'Работа',
    width: 180,
    getCellValue: row => row.job
  },
  {
    name: 'address',
    title: 'Адресс',
    width: 180,
    getCellValue: row => row.address
  },
  {
    name: 'rating',
    title: 'Рейтинг',
    width: 100,
    getCellValue: row => row.rating
  },
  {
    name: 'isVisible',
    title: 'Скрыть',
    width: 85,
    getCellValue: row => row.isVisible
  },
  {
    name: 'cost',
    title: 'Стоимость',
    width: 100,
    getCellValue: row => row.cost
  },
  {
    name: 'description',
    title: 'Описание',
    width: 100,
    getCellValue: row => row.description
  },
  {
    name: 'experience',
    title: 'Опыт работы',
    width: 100,
    getCellValue: row => row.experience
  },
  // {
  //   name: 'photo',
  //   title: 'Фото',
  //   width: 100,
  //   getCellValue: row => (row.photo)
  // },
  {
    name: 'district',
    title: 'Район',
    width: 180,
    getCellValue: row => (row.district ? row.district.name : null),
    availableValues: []
  },
  {
    name: 'createDate',
    title: 'Дата добавления',
    width: 140,
    editingEnabled: false,
    getCellValue: row => row.createDate ? moment(row.createDate).format('DD.MM.YYYY') : ''
  },
];

const initialState = {
  items: [],
  totalCount: 0,
  loading: false,
  lastQueryParams: '',
  gridColumns: gridSettings.map(item => ({
    name: item.name,
    title: item.title,
    getCellValue: item.getCellValue,
    availableValues: item.availableValues
  })),
  gridColumnWidths: gridSettings.map(item => ({
    columnName: item.name,
    width: item.width
  })),
  gridColumnEditing: gridSettings.map(item => ({
    columnName: item.name,
    editingEnabled: item.editingEnabled
  })),
  gridHiddenColumnNames: [
    'id',
    'patronymic',
    'address',
    'cost',
    'description',
    'experience',
    'photo',
    'createDate'
  ],
  gridColumnOrder: gridSettings.map(item => item.name),
  phonesGridColumns: [
    {
      name: 'number',
      title: 'Номер',
      getCellValue: row => row.number
    },
    {
      name: 'operator',
      title: 'Оператор',
      getCellValue: row => row.operator
    }
  ],
  contactsGridColumns: [
    {
      name: 'name',
      title: 'Название',
      getCellValue: row => row.name
    },
    {
      name: 'value',
      title: 'Значение',
      getCellValue: row => row.value
    },
    {
      name: 'contactType',
      title: 'Тип',
      getCellValue: row => (row.contactType ? row.contactType.name : null),
      availableValues: []
    }
  ]
};

export default (state = initialState, action) => {
  const { type, payload, data } = action;

  switch (type) {
    case TUTOR + LOAD_LIST + START:
    case TUTOR + CREATE_ITEM + START:
    case TUTOR + UPDATE_ITEM + START:
    case TUTOR + REMOVE_ITEM + START:
    case TUTOR + UPDATE_TUTORS_SUBJECTS + START:
    case TUTOR + UPDATE_TUTORS_SPECIALIZATIONS + START:
    case PHONE + CREATE_ITEM + START:
    case PHONE + REMOVE_ITEM + START:
    case CONTACT + CREATE_ITEM + START:
    case CONTACT + REMOVE_ITEM + START:
      return { ...state, loading: true };

    case TUTOR + LOAD_LIST + SUCCESS:
      return {
        ...state,
        items: data.items,
        totalCount: data.totalCount,
        loading: false
      };

    case TUTOR + SAVE_QUERY_PARAMS:
      return { ...state, lastQueryParams: payload.params };

    case TUTOR + CREATE_ITEM + SUCCESS:
    case TUTOR + REMOVE_ITEM + SUCCESS:
    case TUTOR + UPDATE_TUTORS_SUBJECTS + SUCCESS:
    case TUTOR + UPDATE_TUTORS_SUBJECTS + FAIL:
    case TUTOR + UPDATE_TUTORS_SPECIALIZATIONS + SUCCESS:
    case TUTOR + UPDATE_TUTORS_SPECIALIZATIONS + FAIL:
    case PHONE + CREATE_ITEM + SUCCESS:
    case PHONE + REMOVE_ITEM + SUCCESS:
    case CONTACT + CREATE_ITEM + SUCCESS:
    case CONTACT + REMOVE_ITEM + SUCCESS:
      return { ...state, loading: false, lastQueryParams: '' };

    case TUTOR + UPDATE_ITEM + SUCCESS:
      const itemsAfterUpdate = state.items.map(item =>
        item.id === payload.id ? { ...item, ...data } : item
      );
      return { ...state, items: itemsAfterUpdate, loading: false };

    case TUTOR + LOAD_LIST + FAIL:
    case TUTOR + CREATE_ITEM + FAIL:
    case TUTOR + UPDATE_ITEM + FAIL:
    case TUTOR + REMOVE_ITEM + FAIL:
    case PHONE + CREATE_ITEM + FAIL:
    case PHONE + REMOVE_ITEM + FAIL:
    case CONTACT + CREATE_ITEM + FAIL:
    case CONTACT + REMOVE_ITEM + FAIL:
      return { ...state, loading: false };

    default:
      return state;
  }
};
