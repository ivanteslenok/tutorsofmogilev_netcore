import _ from 'lodash';

import React, { useState, useEffect } from 'react';
import Paper from '@material-ui/core/Paper';
import {
  PagingState,
  SortingState,
  CustomPaging,
  EditingState,
  RowDetailState
} from '@devexpress/dx-react-grid';
import {
  Grid,
  DragDropProvider,
  VirtualTable,
  TableHeaderRow,
  PagingPanel,
  TableColumnResizing,
  TableColumnReordering,
  ColumnChooser,
  TableColumnVisibility,
  Toolbar,
  TableEditRow,
  TableEditColumn,
  TableFixedColumns,
  TableRowDetail
} from '@devexpress/dx-react-grid-material-ui';
import Loading from '../Loading';
import DeleteConfirmDialog from '../DeleteConfirmDialog';
import IsVisibleTypeProvider from './IsVisibleTypeProvider';
import Command from './Command';
import EditCell from './EditCell';
import RowDetail from './RowDetail';

const columnChooserMessages = {
  showColumnChooser: 'Список столбцов'
};

const DataGrid = props => {
  const pageSizes = [5, 10, 15];

  const [editingRowIds, setEditingRowIds] = useState([]);
  const [addedRows, setAddedRows] = useState([]);
  const [rowChanges, setRowChanges] = useState({});
  const [deletingRows, setDeletingRows] = useState([]);

  useEffect(() => loadDataForGrid());

  const getQueryParams = () => {
    const columnSorting = props.sorting[0];

    const filter = {
      pageSize: props.pageSize,
      pageNumber: props.currentPage + 1
    };

    if (columnSorting) {
      filter.sortBy = columnSorting.columnName;
      filter.sortDirection = columnSorting.direction === 'desc' ? 'desc' : '';
    }

    return (
      '?' +
      _.keys(filter)
        .filter(x => filter[x])
        .map(x => `${x}=${filter[x]}`)
        .join('&')
    );
  };

  const loadDataForGrid = () => {
    const queryParams = getQueryParams();
    const {
      loading,
      loadData,
      lastQueryParams,
      saveQueryParams,
      availableColumnsValuesLoaded,
      loadAvailableValues
    } = props;

    if (loading || queryParams === lastQueryParams) return;

    loadData(queryParams);
    saveQueryParams(queryParams);
    if (!availableColumnsValuesLoaded) loadAvailableValues();
  };

  const cancelDelete = () => setDeletingRows([]);

  const changeAddedRows = addedRows =>
    setAddedRows(
      addedRows.map(row => (_.keys(row).length ? row : props.defaultValues))
    );

  const commitChanges = ({ added, changed, deleted }) => {
    if (added) {
      added.forEach(item => props.create(item));
    }

    if (changed) {
      props.rows.forEach(row => {
        if (changed[row.id]) {
          props.update(row.id, { ...row, ...changed[row.id] });
        }
      });
    }

    setDeletingRows(deleted || deletingRows);
  };

  const deleteRows = () => {
    deletingRows.forEach(rowId => props.remove(rowId));
    cancelDelete();
  };

  const columns = props.gridColumns.map(x =>
    props.availableColumnsValues[x.name]
      ? { ...x, availableValues: props.availableColumnsValues[x.name] }
      : x
  );

  const deleteRow =
    deletingRows[0] && _.find(props.rows, { id: deletingRows[0] });
  const deleteText = props.getDeleteText(deleteRow);

  return (
    <Paper style={{ position: 'relative' }}>
      <Grid rows={props.rows} columns={columns} getRowId={row => row.id}>
        <SortingState
          sorting={props.sorting}
          onSortingChange={props.onSortingChange}
        />
        <PagingState
          currentPage={props.currentPage}
          onCurrentPageChange={props.onCurrentPageChange}
          pageSize={props.pageSize}
          onPageSizeChange={props.onPageSizeChange}
        />
        <CustomPaging totalCount={props.totalCount} />
        <EditingState
          editingRowIds={editingRowIds}
          onEditingRowIdsChange={setEditingRowIds}
          rowChanges={rowChanges}
          onRowChangesChange={setRowChanges}
          addedRows={addedRows}
          onAddedRowsChange={changeAddedRows}
          onCommitChanges={commitChanges}
          columnExtensions={props.gridColumnEditing}
        />
        <RowDetailState />

        <DragDropProvider />
        <IsVisibleTypeProvider for={['isVisible']} />
        <VirtualTable />
        <TableColumnResizing
          defaultColumnWidths={props.gridColumnWidths}
          onColumnWidthsChange={props.onColumnWidthsChange}
        />
        <TableHeaderRow showSortingControls />
        <TableColumnReordering
          defaultOrder={props.gridColumnOrder}
          onOrderChange={props.onColumnOrderChange}
        />
        <TableRowDetail contentComponent={RowDetail} />

        <TableEditRow cellComponent={EditCell} />
        <TableEditColumn
          width={150}
          showAddCommand={!addedRows.length}
          showEditCommand
          showDeleteCommand
          commandComponent={Command}
        />
        <TableFixedColumns leftColumns={[TableEditColumn.COLUMN_TYPE]} />

        <TableColumnVisibility
          hiddenColumnNames={props.hiddenColumnNames}
          onHiddenColumnNamesChange={props.onHiddenColumnNamesChange}
        />
        <Toolbar />
        <ColumnChooser messages={columnChooserMessages} />
        <PagingPanel pageSizes={pageSizes} />
      </Grid>
      {props.loading && <Loading />}
      <DeleteConfirmDialog
        isOpen={!!deletingRows.length}
        text={deleteText}
        handleClose={cancelDelete}
        handleConfirm={deleteRows}
      />
    </Paper>
  );
};

export default DataGrid;
