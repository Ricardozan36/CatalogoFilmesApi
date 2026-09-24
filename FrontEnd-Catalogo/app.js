const API_URL = 'http://localhost:5244/api/Filmes';
const AUTH_URL = 'http://localhost:5244/api/Auth/login';


const modalFilme = document.getElementById("modal-filme");
const modalLogin = document.getElementById("modal-login");
const btnNovoFilme = document.getElementById("btn-novo-filme");
const btnLogin = document.getElementById("btn-login");
const spanFecharFilme = document.getElementsByClassName("fechar")[0];
const spanFecharLogin = document.getElementsByClassName("fechar-login")[0];
const formFilme = document.getElementById("form-filme");
const formLogin = document.getElementById("form-login");


document.addEventListener('DOMContentLoaded', () => {
    verificarEstadoLogin();
    carregarFilmes();
});


btnNovoFilme.onclick = () => modalFilme.style.display = "block";
spanFecharFilme.onclick = () => modalFilme.style.display = "none";
spanFecharLogin.onclick = () => modalLogin.style.display = "none";

window.onclick = (evento) => {
    if (evento.target == modalFilme) modalFilme.style.display = "none";
    if (evento.target == modalLogin) modalLogin.style.display = "none";
}


btnLogin.onclick = () => {
    if(localStorage.getItem('token')) {
        localStorage.removeItem('token');
        verificarEstadoLogin();
        carregarFilmes();
    } else {
        modalLogin.style.display = "block";
    }
};

function verificarEstadoLogin() {
    if(localStorage.getItem('token')) {
        btnLogin.textContent = "Sair (Logout)";
        btnLogin.style.borderColor = "#e50914";
        btnLogin.style.color = "#e50914";
    } else {
        btnLogin.textContent = "Login Diretor";
        btnLogin.style.borderColor = "#e5e5e5";
        btnLogin.style.color = "#e5e5e5";
    }
}


async function carregarFilmes() {
    const grid = document.getElementById('grid-filmes');
    const token = localStorage.getItem('token');
    
    try {
        const resposta = await fetch(API_URL);
        const filmes = await resposta.json();
        grid.innerHTML = '';

        if (filmes.length === 0) {
            grid.innerHTML = '<p style="color: #fff;">Nenhum filme encontrado.</p>';
            return;
        }

        filmes.forEach(filme => {
            const card = document.createElement('div');
            card.className = 'card-filme';
            
            
            const botaoApagar = token ? 
                `<button class="btn-delete" onclick="apagarFilme(${filme.id})" title="Apagar filme">
                    <span class="material-icons">delete</span>
                 </button>` : '';

            card.innerHTML = `
                <div class="card-imagem">
                    <span class="material-icons">theaters</span>
                </div>
                <div class="card-conteudo">
                    ${botaoApagar}
                    <div class="card-titulo">${filme.titulo}</div>
                    <div class="card-diretor">Direção: ${filme.nomeDiretor}</div>
                    <span class="badge-ano">${filme.anoLancamento}</span>
                </div>
            `;
            grid.appendChild(card);
        });
    } catch (erro) {
        grid.innerHTML = `<p style="color: #e50914;">Erro ao carregar a API.</p>`;
    }
}


formFilme.addEventListener('submit', async (evento) => {
    evento.preventDefault();
    const novoFilme = {
        titulo: document.getElementById('titulo').value,
        anoLancamento: parseInt(document.getElementById('anoLancamento').value),
        diretorId: parseInt(document.getElementById('diretorId').value)
    };

    const resposta = await fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(novoFilme)
    });

    if (resposta.ok) {
        formFilme.reset();
        modalFilme.style.display = "none";
        carregarFilmes(); 
    }
});


formLogin.addEventListener('submit', async (evento) => {
    evento.preventDefault();
    const loginDados = {
        username: document.getElementById('username').value,
        password: document.getElementById('password').value
    };

    const resposta = await fetch(AUTH_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(loginDados)
    });

    if (resposta.ok) {
        let tokenRecebido = "";
        const contentType = resposta.headers.get("content-type");
        
        
        if (contentType && contentType.includes("application/json")) {
            const dados = await resposta.json();
            tokenRecebido = dados.token || dados.accessToken || Object.values(dados)[0];
        } else {
            tokenRecebido = await resposta.text();
            tokenRecebido = tokenRecebido.replace(/^"|"$/g, '').trim(); 
        }
        
        
        console.log("Token capturado com sucesso:", tokenRecebido); 
        
        localStorage.setItem('token', tokenRecebido);
        
        modalLogin.style.display = "none";
        verificarEstadoLogin();
        carregarFilmes(); 
    } else {
        alert("Utilizador ou palavra-passe errados!");
    }
});


async function apagarFilme(id) {
    if(!confirm("Tem a certeza que deseja apagar esta obra-prima?")) return;

    const token = localStorage.getItem('token');
    
    const resposta = await fetch(`${API_URL}/${id}`, {
        method: 'DELETE',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    });

    if (resposta.status === 204) {
        carregarFilmes();
    } else if (resposta.status === 401) {
        alert("Sem permissão! O seu token expirou ou é inválido.");
        localStorage.removeItem('token');
        verificarEstadoLogin();
        carregarFilmes();
    }
}