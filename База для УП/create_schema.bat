@echo off
chcp 1251
psql -f "base_dem.sql" postgresql://postgres@localhost:5432/DEM
pause
