import { useState } from 'react';
import { useAuth } from '@/lib/auth';
import { useI18n } from '@/lib/i18n';
import { Button } from '@/components/ui/Button';
import { Input } from '@/components/ui/Form';
import { Building2, Lock, Mail } from 'lucide-react';

export function LoginPage() {
  const { signIn } = useAuth();
  const { t } = useI18n();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setLoading(true);
    const { error } = await signIn(email, password);
    if (error) setError(error);
    setLoading(false);
  };

  const fillDemo = (demoEmail: string) => {
    setEmail(demoEmail);
    setPassword('demo1234');
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-900 via-slate-800 to-slate-900 flex items-center justify-center p-4">
      <div className="w-full max-w-md">
        <div className="text-center mb-8">
          <div className="inline-flex items-center justify-center w-16 h-16 rounded-2xl bg-blue-600 mb-4">
            <Building2 size={32} className="text-white" />
          </div>
          <h1 className="text-2xl font-bold text-white">Nusa Hospitality PMS</h1>
          <p className="text-slate-400 mt-1">Hotel Property Management System</p>
        </div>

        <div className="bg-white rounded-xl shadow-2xl p-8">
          <form onSubmit={handleSubmit} className="flex flex-col gap-4">
            <div>
              <label className="text-sm font-medium text-slate-700 mb-1 block">{t('auth.email')}</label>
              <div className="relative">
                <Mail size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
                <input
                  type="email"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  className="w-full rounded-lg border border-slate-300 pl-10 pr-3 py-2.5 text-sm outline-none focus:ring-2 focus:ring-blue-500"
                  placeholder="admin@nusahospitality.id"
                  required
                />
              </div>
            </div>
            <div>
              <label className="text-sm font-medium text-slate-700 mb-1 block">{t('auth.password')}</label>
              <div className="relative">
                <Lock size={18} className="absolute left-3 top-1/2 -translate-y-1/2 text-slate-400" />
                <input
                  type="password"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  className="w-full rounded-lg border border-slate-300 pl-10 pr-3 py-2.5 text-sm outline-none focus:ring-2 focus:ring-blue-500"
                  placeholder="••••••••"
                  required
                />
              </div>
            </div>
            {error && (
              <div className="bg-red-50 border border-red-200 rounded-lg px-4 py-3 text-sm text-red-600">
                {error}
              </div>
            )}
            <Button type="submit" loading={loading} className="w-full" size="lg">
              {loading ? t('auth.signing_in') : t('auth.login_button')}
            </Button>
          </form>

          <div className="mt-6 pt-6 border-t border-slate-100">
            <p className="text-xs text-slate-500 font-medium mb-3">{t('auth.demo_accounts')} (password: demo1234)</p>
            <div className="grid grid-cols-1 gap-2">
              <button onClick={() => fillDemo('admin@nusahospitality.id')} className="text-left text-xs bg-slate-50 hover:bg-slate-100 rounded-lg px-3 py-2 transition-colors">
                <span className="font-medium text-slate-700">{t('auth.admin')}:</span> <span className="text-slate-500">admin@nusahospitality.id</span>
              </button>
              <button onClick={() => fillDemo('manager@nusahospitality.id')} className="text-left text-xs bg-slate-50 hover:bg-slate-100 rounded-lg px-3 py-2 transition-colors">
                <span className="font-medium text-slate-700">{t('auth.manager')}:</span> <span className="text-slate-500">manager@nusahospitality.id</span>
              </button>
              <button onClick={() => fillDemo('reception@nusahospitality.id')} className="text-left text-xs bg-slate-50 hover:bg-slate-100 rounded-lg px-3 py-2 transition-colors">
                <span className="font-medium text-slate-700">{t('auth.receptionist')}:</span> <span className="text-slate-500">reception@nusahospitality.id</span>
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
