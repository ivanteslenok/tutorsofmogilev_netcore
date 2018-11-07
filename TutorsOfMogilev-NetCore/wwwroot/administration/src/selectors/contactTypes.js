import _ from 'lodash';
import { createSelector } from 'reselect';

const getContactTypes = store => store.contactTypes.items;

export const getSortedContactTypes = createSelector(
  [getContactTypes],
  (contactTypes) => _.sortBy(contactTypes, distr => distr['name'])
);
