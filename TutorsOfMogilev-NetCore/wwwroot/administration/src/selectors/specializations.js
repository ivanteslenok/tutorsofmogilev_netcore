import _ from 'lodash';
import { createSelector } from 'reselect';

const getSpecializations = store => store.specializations.items;

export const getSortedSpecializations = createSelector(
  [getSpecializations],
  (specializations) => _.sortBy(specializations, distr => distr['name'])
);
