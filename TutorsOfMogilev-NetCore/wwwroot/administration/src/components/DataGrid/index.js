import _ from 'lodash';

import React, { PureComponent } from 'react';
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

export default class DataGrid extends PureComponent {
  state = {
    columns: [],
    defaultColumnWidths: [],
    defaultHiddenColumnNames: [],
    rows: [],
    sorting: [],
    editingRowIds: [],
    addedRows: [],
    rowChanges: {},
    deletingRows: [],
    totalCount: 0,
    pageSize: 10,
    pageSizes: [5, 10, 15],
    currentPage: 0,
    loading: true
  };

  static getDerivedStateFromProps(props) {
    return {
      columns: props.columns,
      defaultColumnWidths: props.defaultColumnWidths,
      defaultHiddenColumnNames: props.defaultHiddenColumnNames,
      defaultColumnOrder: props.defaultColumnOrder,
      availableColumnsValues: props.availableColumnsValues,
      defaultValues: props.defaultValues,
      rows: props.rows,
      totalCount: props.totalCount,
      loading: props.loading
    };
  }

  componentDidMount = () => {
    const { availableColumnsValuesLoaded, loadAvailableValues } = this.props;

    this.loadDataForGrid();
    if (!availableColumnsValuesLoaded) loadAvailableValues();
  };

  componentDidUpdate = () => this.loadDataForGrid();

  getQueryParams() {
    const { sorting, pageSize, currentPage } = this.state;
    const columnSorting = sorting[0];

    const filter = {
      pageSize,
      pageNumber: currentPage + 1
    };

    if (columnSorting) {
      filter.sortBy = columnSorting.columnName;
      filter.sortDirection = columnSorting.direction === 'desc' ? 'desc' : ''
    }

    let queryParams = '?';

    for (let key in filter) {
      queryParams += filter[key] ? `${key}=${filter[key]}&` : '';
    }

    // чтобы убрать последний & или ? если нету параметров
    queryParams = queryParams.substring(0, queryParams.length - 1);

    return queryParams;
  }

  loadDataForGrid() {
    const queryParams = this.getQueryParams();
    const { loading, loadData, lastQueryParams, saveQueryParams } = this.props;

    if (loading || queryParams === lastQueryParams) return;

    loadData(queryParams);
    saveQueryParams(queryParams);
  }

  changeSorting = sorting => {
    this.setState({
      loading: true,
      sorting
    });
  };

  changeCurrentPage = currentPage => {
    this.setState({
      loading: true,
      currentPage
    });
  };

  changePageSize = pageSize => {
    const { totalCount, currentPage: stateCurrentPage } = this.state;
    const totalPages = Math.ceil(totalCount / pageSize);
    const currentPage = Math.min(stateCurrentPage, totalPages - 1);

    this.setState({
      loading: true,
      pageSize,
      currentPage
    });
  };

  changeEditingRowIds = editingRowIds => this.setState({ editingRowIds });

  changeRowChanges = rowChanges => this.setState({ rowChanges });

  cancelDelete = () => this.setState({ deletingRows: [] });

  changeAddedRows = addedRows =>
    this.setState(state => {
      return {
        addedRows: addedRows.map(row =>
          _.keys(row).length ? row : state.defaultValues
        )
      };
    });

  commitChanges = ({ added, changed, deleted }) => {
    let { rows } = this.state;
    const { deletingRows } = this.state;

    if (added) {
      added.forEach(item => this.props.create(item));
    }

    if (changed) {
      rows = rows.forEach(row => {
        if (changed[row.id]) {
          this.props.update(row.id, { ...row, ...changed[row.id] });
        }
      });
    }

    this.setState({ rows, deletingRows: deleted || deletingRows });
  };

  deleteRows = () => {
    const { deletingRows } = this.state;
    deletingRows.forEach(rowId => this.props.remove(rowId));
    this.setState({ deletingRows: [] });
  };

  render() {
    const {
      rows,
      editingRowIds,
      addedRows,
      rowChanges,
      deletingRows,
      defaultColumnWidths,
      defaultHiddenColumnNames,
      defaultColumnOrder,
      sorting,
      pageSize,
      pageSizes,
      currentPage,
      totalCount,
      loading,
      availableColumnsValues
    } = this.state;

    let { columns } = this.state;

    const deleteRow = deletingRows[0] && _.find(rows, { id: deletingRows[0] });
    const deleteText = this.props.getDeleteText(deleteRow);

    columns.forEach(x => {
      if (availableColumnsValues[x.name])
        x.availableValues = availableColumnsValues[x.name];
    });

    return (
      <Paper style={{ position: 'relative' }}>
        <Grid rows={rows} columns={columns} getRowId={row => row.id}>
          <SortingState
            sorting={sorting}
            onSortingChange={this.changeSorting}
          />
          <PagingState
            currentPage={currentPage}
            onCurrentPageChange={this.changeCurrentPage}
            pageSize={pageSize}
            onPageSizeChange={this.changePageSize}
          />
          <CustomPaging totalCount={totalCount} />
          <EditingState
            editingRowIds={editingRowIds}
            onEditingRowIdsChange={this.changeEditingRowIds}
            rowChanges={rowChanges}
            onRowChangesChange={this.changeRowChanges}
            addedRows={addedRows}
            onAddedRowsChange={this.changeAddedRows}
            onCommitChanges={this.commitChanges}
          />
          <RowDetailState />

          <DragDropProvider />
          <IsVisibleTypeProvider for={['isVisible']} />
          <VirtualTable />
          <TableColumnReordering defaultOrder={defaultColumnOrder} />
          <TableColumnResizing defaultColumnWidths={defaultColumnWidths} />
          <TableHeaderRow showSortingControls />
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
            defaultHiddenColumnNames={defaultHiddenColumnNames}
          />
          <Toolbar />
          <ColumnChooser messages={columnChooserMessages} />
          <PagingPanel pageSizes={pageSizes} />
        </Grid>
        {loading && <Loading />}
        <DeleteConfirmDialog
          isOpen={!!deletingRows.length}
          text={deleteText}
          handleClose={this.cancelDelete}
          handleConfirm={this.deleteRows}
        />
      </Paper>
    );
  }
}
