import React, { useEffect } from 'react';
import Paper from '@material-ui/core/Paper';
import FilterSelect from './FilterSelect';

const TutorsFilter = props => {
  useEffect(() => {
    const { isLookupValuesLoaded, loadLookupValues } = props;

    if (!isLookupValuesLoaded && loadLookupValues) loadLookupValues();
  }, []);

  return (
    <Paper>
      <FilterSelect
        items={props.cities}
        itemId={props.cityId}
        setItemId={props.setCityId}
        placeholder="Выберите город"
      />
      <FilterSelect
        items={props.subjects}
        itemId={props.subjectId}
        setItemId={props.setSubjectId}
        placeholder="Выберите предмет"
      />
    </Paper>
  );
};

export default TutorsFilter;
