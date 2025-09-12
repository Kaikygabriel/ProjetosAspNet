using BlibiotecaApi.Data;
using BlibiotecaApi.Model;
using BlibiotecaApi.Repository.Interfaces;

namespace BlibiotecaApi.Repository;


public class BlibiotecaRepository(BlibiotecaContextApi context) :Repository<Blibioteca>(context),IBlibiotecaRepository;